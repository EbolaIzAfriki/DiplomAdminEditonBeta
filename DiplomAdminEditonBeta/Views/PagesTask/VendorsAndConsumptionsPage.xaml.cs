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
    /// Логика взаимодействия для ChosePostAndConsumptions.xaml
    /// </summary>
    public partial class VendorsAndConsumptionsPage : Page
    {
        public static int CountVendors = 0, CountConsumptions = 0;
        public static bool IsModificate = true;

        public  static List<Client> ChosedClient, NotChosedClients;
        private List<int> IdClients;

        PointsPage PointsPage;
        MainWorkOnTaskForm MainWorkOnTaskForm;
        private bool IsInitializaFinished = false;
        public VendorsAndConsumptionsPage(PointsPage pointsPage, MainWorkOnTaskForm mainWorkOnTaskForm)
        {
            InitializeComponent();
            PointsPage = pointsPage;
            MainWorkOnTaskForm = mainWorkOnTaskForm;
            
            /*if(MainWorkOnTaskForm.DBTask != null)
            {
                //Инициализируем список выбранных не выбранных клиентов а так же массив id выбранных клиентов
                ChosedClient = MainWorkOnTaskForm.DBTask.Point.Select(p => p.Client).Distinct().ToList();
                IdClients = ChosedClient.Select(d => d.Id).ToList();
                NotChosedClients = DiplomBetaDBEntities.GetContext().Client.Where(p => !IdClients.Contains(p.Id)).ToList();

                //Получаем число потребителей и поставщиков и заполняем текстовые поля
                CountConsumptions = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 1).ToList().Count;
                CountVendors = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 2).ToList().Count;
                CountConsumptionsTB.Text = CountConsumptions.ToString();
                CountVendorsTB.Text = CountVendors.ToString();
            }
            else
            {
                //Инициализируем список выбранных не выбранных клиентов а так же массив id выбранных клиентов
                ChosedClient = new List<Client>();
                IdClients = new List<int>();
                NotChosedClients = DiplomBetaDBEntities.GetContext().Client.ToList();

                //Получаем число потребителей и поставщиков и заполняем текстовые поля
                CountConsumptions = 0;
                CountVendors = 0;
                CountConsumptionsTB.Text = CountConsumptions.ToString();
                CountVendorsTB.Text = CountVendors.ToString();
            }

            ChoseConsumprionDataGrid.ItemsSource = ChosedClient.Where(p => p.TypeId == 1).ToList();
            AllConsumptionsDataGrid.ItemsSource = NotChosedClients.Where(p => p.TypeId == 1).ToList();

            ChoseVendorDataGrid.ItemsSource = ChosedClient.Where(p => p.TypeId == 2).ToList();
            AllVendorsDatagrid.ItemsSource = NotChosedClients.Where(p => p.TypeId == 2).ToList();
            IsInitializaFinished = true;
            EnablePointPage();*/
        }

        public void UpdateDataGridConsumptions()
        {
            ChoseConsumprionDataGrid.ItemsSource = null;
            AllConsumptionsDataGrid.ItemsSource = null;

            ChoseConsumprionDataGrid.ItemsSource = ChosedClient.Where(p => p.TypeId == 1).ToList();
            AllConsumptionsDataGrid.ItemsSource = NotChosedClients.Where(p => p.TypeId == 1).ToList();
        }

        public void UpdateDataGridVendors()
        {
            ChoseVendorDataGrid.ItemsSource = null;
            AllVendorsDatagrid.ItemsSource = null;

            ChoseVendorDataGrid.ItemsSource = ChosedClient.Where(p => p.TypeId == 2).ToList();
            AllVendorsDatagrid.ItemsSource = NotChosedClients.Where(p => p.TypeId == 2).ToList();
        }

        private void AddConsumprionButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllConsumptionsDataGrid.SelectedItem == null)
                return;

            ChosedClient.Add(AllConsumptionsDataGrid.SelectedItem as Client);
            NotChosedClients.Remove(AllConsumptionsDataGrid.SelectedItem as Client);

            IdClients = ChosedClient.Select(d => d.Id).ToList();
            UpdateDataGridConsumptions();
            EnablePointPage();
        }

        private void DeleteConsumptionButton_Click(object sender, RoutedEventArgs e)
        {
           /* if (ChoseConsumprionDataGrid.SelectedItem == null)
                return;

            NotChosedClients.Add(ChoseConsumprionDataGrid.SelectedItem as Client);
            ChosedClient.Remove(ChoseConsumprionDataGrid.SelectedItem as Client);

            IdClients = ChosedClient.Select(d => d.Id).ToList();


            if (MainWorkOnTaskForm.DBTask != null)
            {
                if (MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 1).ToList().Count > 0)
                {
                    Client client = ChoseConsumprionDataGrid.SelectedItem as Client;
                    Client DifferentClient = DiplomBetaDBEntities.GetContext().Client.FirstOrDefault(p => IdClients.Contains(p.Id) && p.TypeId == 1);
                    foreach (Point point in MainWorkOnTaskForm.DBTask.Point)
                    {
                        if (point.Client.Id == (ChoseConsumprionDataGrid.SelectedItem as Client).Id)
                        {
                            point.Client = DifferentClient;
                        }
                    }
                    DiplomBetaDBEntities.GetContext().SaveChanges();
                }
            }
            UpdateDataGridConsumptions();
            EnablePointPage();*/
        }

        private void AddVendorButton_Click(object sender, RoutedEventArgs e)
        {
            if (AllVendorsDatagrid.SelectedItem == null)
                return;

            ChosedClient.Add(AllVendorsDatagrid.SelectedItem as Client);
            NotChosedClients.Remove(AllVendorsDatagrid.SelectedItem as Client);

            IdClients = ChosedClient.Select(d => d.Id).ToList();
            UpdateDataGridVendors();
            EnablePointPage();
        }

        private void DeleteVendorButton_Click(object sender, RoutedEventArgs e)
        {
           /* if (ChoseVendorDataGrid.SelectedItem == null)
                return;

            NotChosedClients.Add(ChoseVendorDataGrid.SelectedItem as Client);
            ChosedClient.Remove(ChoseVendorDataGrid.SelectedItem as Client);

            IdClients = ChosedClient.Select(d => d.Id).ToList();

            if(MainWorkOnTaskForm.DBTask != null)
            {
                if (MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 2).ToList().Count > 0)
                {
                    Client client = ChoseVendorDataGrid.SelectedItem as Client;
                    Client DifferentClient = DiplomBetaDBEntities.GetContext().Client.FirstOrDefault(p => IdClients.Contains(p.Id) && p.TypeId == 2);
                    foreach (Point point in MainWorkOnTaskForm.DBTask.Point)
                    {
                        if (point.Client.Id == (ChoseVendorDataGrid.SelectedItem as Client).Id)
                        {
                            point.Client = DifferentClient;
                        }
                    }
                    DiplomBetaDBEntities.GetContext().SaveChanges();
                }
            }
            UpdateDataGridVendors();
            EnablePointPage();*/
        }

        private void CountConsumptionsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsInitializaFinished)
                return;

            if (MainWorkOnTaskForm.DBTask == null)
            {
                /*DiplomBetaDBEntities.GetContext().Task.Add(new Task() {Status = DiplomBetaDBEntities.GetContext().Status.First(), Point = new List<Point>(), User = MainForm.CurrentUser, Constraint = new List<Constraint>() });
                DiplomBetaDBEntities.GetContext().SaveChanges();
                MainWorkOnTaskForm.DBTask = DiplomBetaDBEntities.GetContext().Task.ToList().Last();
                MainWorkOnTaskForm.mainWorkStaticForm.StatusTB.Text = "Статус: " + MainWorkOnTaskForm.DBTask.Status.Name;*/
            }

            IsModificate = true;
            CountConsumptions = Int32.Parse(CountConsumptionsTB.Text);
            MainWorkOnTaskForm.mainWorkStaticForm.TarifsRB.IsEnabled = CountVendors > 0 && CountConsumptions > 0;

            int CountConsPoint = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 1).ToList().Count;
            if (CountConsPoint < CountConsumptions)
            {
               /* for(int i = 0; i < CountConsumptions- CountConsPoint; i++)
                {
                    int pos = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 1).ToList().Count + 1;
                    MainWorkOnTaskForm.DBTask.Point.Add(new Point() { Task = MainWorkOnTaskForm.DBTask, Address = "", Client = ChosedClient.FirstOrDefault(p => p.TypeId == 1), Name = "Магазин " + pos, Position = pos, ProductCount = 0 });
                    DiplomBetaDBEntities.GetContext().SaveChanges();
                }*/
            }
            else
            {
                /*for (int i = 0; i > CountConsumptions - CountConsPoint; i--)
                {
                    DiplomBetaDBEntities.GetContext().Point.Remove(MainWorkOnTaskForm.DBTask.Point.Last(p => p.Client.TypeId == 1));
                    DiplomBetaDBEntities.GetContext().SaveChanges();
                }*/
            }
            EnablePointPage();
        }

        private void CountVendorsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsInitializaFinished)
                return;

            if (MainWorkOnTaskForm.DBTask == null)
            {
                /*DiplomBetaDBEntities.GetContext().Task.Add(new Task() { Status = DiplomBetaDBEntities.GetContext().Status.First(), Point = new List<Point>(), User = MainForm.CurrentUser, Constraint = new List<Constraint>() });
                DiplomBetaDBEntities.GetContext().SaveChanges();
                MainWorkOnTaskForm.DBTask = DiplomBetaDBEntities.GetContext().Task.ToList().Last();
                MainWorkOnTaskForm.mainWorkStaticForm.StatusTB.Text = "Статус: " + MainWorkOnTaskForm.DBTask.Status.Name;*/
            }

            IsModificate = true;
            CountVendors = Int32.Parse(CountVendorsTB.Text);
            MainWorkOnTaskForm.mainWorkStaticForm.TarifsRB.IsEnabled = CountVendors > 0 && CountConsumptions > 0;

            int CountVendPoint = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 2).ToList().Count;
            if (CountVendPoint < CountVendors)
            {
                for (int i = 0; i < CountVendors - CountVendPoint; i++)
                {
                    /*int pos = MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 2).ToList().Count + 1;
                    MainWorkOnTaskForm.DBTask.Point.Add(new Point() { Task = MainWorkOnTaskForm.DBTask, Address = "", Client = ChosedClient.FirstOrDefault(p => p.TypeId == 2), Name = "Склад " + pos, Position = pos, ProductCount = 0 });
                    DiplomBetaDBEntities.GetContext().SaveChanges();*/
                }
            }
            else
            {
                for (int i = 0; i > CountVendors - CountVendPoint; i--)
                {
                    /*DiplomBetaDBEntities.GetContext().Point.Remove(MainWorkOnTaskForm.DBTask.Point.Last(p => p.Client.TypeId == 2));
                    DiplomBetaDBEntities.GetContext().SaveChanges();*/
                }
            }

            EnablePointPage();
        }

        private void EnablePointPage()
        {
            if (MainWorkOnTaskForm.DBTask == null)
                return;
            if (MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 1).ToList().Count > 0 && MainWorkOnTaskForm.DBTask.Point.Where(p => p.Client.TypeId == 2).ToList().Count > 0 && ChoseVendorDataGrid.Items.Count > 0 && ChoseConsumprionDataGrid.Items.Count > 0)
            {
                MainWorkOnTaskForm.PointsRB.IsEnabled = true;
                PointsPage.UpdateDataGrid();

                if(!MainWorkOnTaskForm.DBTask.Point.Select(p => p.ProductCount).Contains(0))
                {
                    MainWorkOnTaskForm.TarifsRB.IsEnabled = true;
                }
                else
                {
                    MainWorkOnTaskForm.TarifsRB.IsEnabled = false;
                }
            }
            else
            {
                MainWorkOnTaskForm.PointsRB.IsEnabled = false;
                MainWorkOnTaskForm.TarifsRB.IsEnabled = false;
            }
        } 
    }
}
