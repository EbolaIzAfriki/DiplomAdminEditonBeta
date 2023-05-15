using DiplomAdminEditonBeta.Matrix_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DiplomAdminEditonBeta.Views.PagesTask
{
    /// <summary>
    /// Логика взаимодействия для TarifsAndPointsPage.xaml
    /// </summary>
    public partial class TarifsAndPointsPage : Page
    {
        public MatrixModel TarifMatrix { get; set; }

        private bool SaveProfile = false;
        public int LastColumns = 0, LastRows = 0;

        MainWorkOnTaskForm MainWorkOnTaskForm;
        public TarifsAndPointsPage(MainWorkOnTaskForm mainWorkOnTaskForm)
        {
            InitializeComponent();
            MainWorkOnTaskForm = mainWorkOnTaskForm;
        }

        public void UpdateMatrix()
        {
            if (SaveProfile)
            {
                LastColumns = TarifMatrix.Columns;
                LastRows = TarifMatrix.Rows;

                TarifMatrix = new MatrixModel() { Columns = VendorsAndConsumptionsPage.CountConsumptions + 2, Rows = VendorsAndConsumptionsPage.CountVendors + 2 };

                TarifsMatrixLV.DataContext = null;
                TarifsMatrixLV.ItemsSource = null;
            }
            else
            {
                TarifMatrix = new MatrixModel() { Columns = VendorsAndConsumptionsPage.CountConsumptions + 2, Rows = VendorsAndConsumptionsPage.CountVendors + 2 };
                LastColumns = TarifMatrix.Columns;
                LastRows = TarifMatrix.Rows;
                SaveProfile = true;
            }
            FillMatrix();
            TarifsMatrixLV.DataContext = TarifMatrix;
            TarifsMatrixLV.ItemsSource = TarifMatrix.Items;
        }

        public void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            int Counter = 0;
            bool Flag = true;
            for (int i = 1; i < TarifMatrix.Rows - 1; i++)
            {
                for (int j = 1; j < TarifMatrix.Columns - 1; j++)
                {
                    int position = TarifMatrix.Columns * i + j;
                    if (Flag)
                    {
                        if (int.Parse(TarifMatrix.Items[position].Value) != transportationCosts[Counter].Value)
                        {
                            transportationCosts[Counter].Value = int.Parse(TarifMatrix.Items[position].Value);
                            DiplomBetaDBEntities.GetContext().SaveChanges();
                            Flag = false;
                        }
                    }
                    if(transportationCosts[Counter].Value == 0)
                    {
                        MainWorkOnTaskForm.ConclusionRB.IsEnabled = false;
                    }
                    Counter++;
                }
            }
            MainWorkOnTaskForm.ConclusionRB.IsEnabled = true;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                labalFoc.Focus();
            }
        }
        List<TransportationCost> transportationCosts = new List<TransportationCost>();
        public void FillMatrix()
        {
            TarifMatrix = new MatrixModel() { Columns = VendorsAndConsumptionsPage.CountConsumptions + 2, Rows = VendorsAndConsumptionsPage.CountVendors + 2 };
            TarifsMatrixLV.DataContext = null;
            TarifsMatrixLV.ItemsSource = null;

            //Наконец Заполнение поставки
            int CurrentPositionCellInDatabase = -1, PositonInTransCost = 0;
            if (MainWorkOnTaskForm.DBTask.TransportationCost.Count > 0)
            {
                transportationCosts = MainWorkOnTaskForm.DBTask.TransportationCost.OrderBy(p => p.IdPosition).ToList();

                //Вывод (Создание новыых поставок) в соответствии с изменениями
                if(MainWorkOnTaskForm.DBTask.CountColumn != TarifMatrix.Columns-2 || MainWorkOnTaskForm.DBTask.CountRow != TarifMatrix.Rows-2)
                {
                    int CountTransCost = transportationCosts.Count-1;
                    List<TransportationCost> SaveTransportationCosts = new List<TransportationCost>();
                    List<TransportationCost> DeleteTransportationCosts = new List<TransportationCost>();

                    for (int i = 1; i < TarifMatrix.Rows - 1; i++)
                    {
                        for (int j = 1; j < TarifMatrix.Columns - 1; j++)
                        {
                            CurrentPositionCellInDatabase++;
                            int position = TarifMatrix.Columns * i + j;

                            if (j == MainWorkOnTaskForm.DBTask.CountColumn+1 || i == MainWorkOnTaskForm.DBTask.CountRow+1 || !SaveProfile)
                            {
                                foreach(TransportationCost transportationCost in transportationCosts)
                                {
                                    if(transportationCost.IdPosition >= CurrentPositionCellInDatabase)
                                    {
                                        transportationCost.IdPosition++;
                                    }
                                }
                                SaveTransportationCosts.Add(new TransportationCost() { Value = 0, IdPosition = CurrentPositionCellInDatabase });
                                TarifMatrix.Items[position].Value = "0";

                                continue;
                            }
                            if (PositonInTransCost >= transportationCosts.Count)
                            {
                                SaveTransportationCosts.Add(new TransportationCost() { Value = 0, IdPosition = CurrentPositionCellInDatabase });
                                TarifMatrix.Items[position].Value = "0";
                                continue;
                            }
                            DeleteTransportationCosts.Add(transportationCosts[PositonInTransCost]);
                            TarifMatrix.Items[position].Value = transportationCosts[PositonInTransCost].Value.ToString();
                            PositonInTransCost++;
                        }
                        if (MainWorkOnTaskForm.DBTask.CountColumn > TarifMatrix.Columns - 2)
                        {
                            int p = MainWorkOnTaskForm.DBTask.CountColumn.Value;
                            PositonInTransCost  +=  p - (TarifMatrix.Columns - 2);
                        }

                    }
                    Task task = DiplomBetaDBEntities.GetContext().Task.FirstOrDefault(p => p.Id == MainWorkOnTaskForm.DBTask.Id);
                    task.CountColumn = TarifMatrix.Columns - 2;
                    task.CountRow = TarifMatrix.Rows - 2;
                    if ((transportationCosts.Count+SaveTransportationCosts.Count) == (TarifMatrix.Rows - 2) * (TarifMatrix.Columns - 2))
                    {
                        DiplomBetaDBEntities.GetContext().TransportationCost.RemoveRange(transportationCosts);
                        DiplomBetaDBEntities.GetContext().SaveChanges();
                        transportationCosts.AddRange(SaveTransportationCosts);
                        transportationCosts = transportationCosts.OrderBy(p => p.IdPosition).ToList();
                        task.TransportationCost = transportationCosts;
                        DiplomBetaDBEntities.GetContext().TransportationCost.AddRange(SaveTransportationCosts);
                    }
                    else
                    {
                        int n = 0;
                        foreach(TransportationCost transportationCost in DeleteTransportationCosts)
                        {
                            transportationCost.IdPosition = n;
                            n++;
                        }
                        DiplomBetaDBEntities.GetContext().TransportationCost.RemoveRange(transportationCosts);
                        DiplomBetaDBEntities.GetContext().SaveChanges();
                        DeleteTransportationCosts = DeleteTransportationCosts.OrderBy(p => p.IdPosition).ToList();
                        task.TransportationCost = DeleteTransportationCosts;
                        DiplomBetaDBEntities.GetContext().TransportationCost.AddRange(DeleteTransportationCosts);
                    }
                }
                else
                {
                    transportationCosts = transportationCosts.OrderBy(p => p.IdPosition).ToList();
                    for (int i = 1; i < TarifMatrix.Rows - 1; i++)
                    {
                        for (int j = 1; j < TarifMatrix.Columns - 1; j++)
                        {
                            CurrentPositionCellInDatabase++;
                            int position = TarifMatrix.Columns * i + j;
                            TarifMatrix.Items[position].Value = transportationCosts[CurrentPositionCellInDatabase].Value.ToString();
                        }
                    }
                }
                DiplomBetaDBEntities.GetContext().SaveChanges();
                //transportationCosts = DiplomBetaDBEntities.GetContext().Task.FirstOrDefault(p => p.Id == MainWorkOnTaskForm.DBTask.Id).TransportationCost.ToList();

                TarifsMatrixLV.DataContext = TarifMatrix;
                TarifsMatrixLV.ItemsSource = TarifMatrix.Items;
            }
            else
            {
                transportationCosts = new List<TransportationCost>();
                for (int i = 1; i < TarifMatrix.Rows - 1; i++)
                {
                    for (int j = 1; j < TarifMatrix.Columns - 1; j++)
                    {
                        CurrentPositionCellInDatabase++;
                        int position = TarifMatrix.Columns * i + j;
                        transportationCosts.Add(new TransportationCost() { Value = 0, IdPosition = CurrentPositionCellInDatabase });
                        TarifMatrix.Items[position].Value = "0";
                    }
                }
                Task task = DiplomBetaDBEntities.GetContext().Task.FirstOrDefault(p => p.Id == MainWorkOnTaskForm.DBTask.Id);
                task.CountColumn = VendorsAndConsumptionsPage.CountConsumptions;
                task.CountRow = VendorsAndConsumptionsPage.CountVendors;
                task.TransportationCost = transportationCosts;
                DiplomBetaDBEntities.GetContext().TransportationCost.AddRange(transportationCosts);
            }
            DiplomBetaDBEntities.GetContext().SaveChanges();




            TarifMatrix.Items[0].Value = "Наименование пунктов";
            TarifMatrix.Items[0].IsNotEnable = true;
            TarifMatrix.Items[TarifMatrix.Items.Count - 1].IsNotEnable = true;

            TarifMatrix.Items[TarifMatrix.Columns - 1].Value = "Запасы";
            TarifMatrix.Items[TarifMatrix.Columns - 1].IsNotEnable = true;

            TarifMatrix.Items[TarifMatrix.Columns * (TarifMatrix.Rows - 1)].Value = "Потребности";
            TarifMatrix.Items[TarifMatrix.Columns * (TarifMatrix.Rows - 1)].IsNotEnable = true;

            List<Point> ConsumersNames = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 1).ToList();
            List<Point> VendorsNames = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 2).ToList();

            //Заполнение наименований
            for (int i = 1; i < TarifMatrix.Columns - 1; i++)
            {
                TarifMatrix.Items[i].Value = ConsumersNames[i - 1].Name;
                TarifMatrix.Items[i].IsNotEnable = true;
            }
            for (int j = 1; j < TarifMatrix.Rows - 1; j++)
            {

                TarifMatrix.Items[TarifMatrix.Columns * j].Value = VendorsNames[j - 1].Name;
                TarifMatrix.Items[TarifMatrix.Columns * j].IsNotEnable = true;
            }

            //Заполнение состояние складов
            for (int i = 1; i < TarifMatrix.Columns - 1; i++)
            {
                TarifMatrix.Items[TarifMatrix.Columns * (TarifMatrix.Rows - 1) + i].Value = ConsumersNames[i - 1].ProductCount.ToString();
                TarifMatrix.Items[TarifMatrix.Columns * (TarifMatrix.Rows - 1) + i].IsNotEnable = true;
            }
            for (int j = 2; j < TarifMatrix.Rows; j++)
            {
                TarifMatrix.Items[TarifMatrix.Columns * j - 1].Value = VendorsNames[j - 2].ProductCount.ToString();
                TarifMatrix.Items[TarifMatrix.Columns * j - 1].IsNotEnable = true;
            }
        }
    }
}
