using DiplomAdminEditonBeta.Views.PagesTask;
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
using System.Windows.Shapes;

namespace DiplomAdminEditonBeta.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWorkOnTaskForm.xaml
    /// </summary>
    public partial class MainWorkOnTaskForm : Window
    {
        public static MainWorkOnTaskForm mainWorkStaticForm;
        public MainForm MainForm;

        public static Task DBTask = null;
        public MainWorkOnTaskForm(MainForm mainForm, Task task)
        {
            DBTask = task;
            NeedServisesAndChoseCarrierPage = new NeedServisesAndChoseCarrierPage();
            CreateConstrainPage = new ConstrainsPage();
            if (DBTask.User == null)
            {
                DBTask.User = MainForm.CurrentUser;
                DiplomBetaDBEntities.GetContext().SaveChanges();
            }

            InitializeComponent();

            MainForm = mainForm;
            VendorsAndConsumptionsPage.IsModificate = true;

            PointsPage = new PointsPage(this);
            VendorsAndConsumptionsPage = new VendorsAndConsumptionsPage(PointsPage, this);
            TarifsAndPointsPage = new TarifsAndPointsPage(this);

            StatusTB.Text = "Статус: " + DBTask.Status.Name;

            mainWorkStaticForm = this;
            MainFrame.Content = VendorsAndConsumptionsPage;
            StageTB.Text = "Этап 1: Добавление участников транспортной задачи";
            if (DBTask.TransportationCost.Count != 0)
            {
                if (DBTask.Conclusion != "")
                    ConclusionPage.ConclusionTB.Text = DBTask.Conclusion;
                int Counter = 0;
                for (int i = 0; i < DBTask.CountRow; i++)
                {
                    for (int j = 0; j < DBTask.CountColumn; j++)
                    {
                        if (DBTask.TransportationCost.ToList()[Counter].Value == 0)
                        {
                            ConclusionRB.IsEnabled = false;
                            return;
                        }
                        Counter++;
                    }
                }
                ConclusionRB.IsEnabled = true;
            }

        }

        public MainWorkOnTaskForm(MainForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;

            PointsPage = new PointsPage(this);
            VendorsAndConsumptionsPage = new VendorsAndConsumptionsPage(PointsPage, this);
            TarifsAndPointsPage = new TarifsAndPointsPage(this);

            mainWorkStaticForm = this;
            MainFrame.Content = VendorsAndConsumptionsPage;
            StageTB.Text = "Этап 1: Добавление участников транспортной задачи";
            NeedServisesAndChoseCarrierPage = new NeedServisesAndChoseCarrierPage();
        }

        NeedServisesAndChoseCarrierPage NeedServisesAndChoseCarrierPage;
        TarifsAndPointsPage TarifsAndPointsPage;
        ConstrainsPage CreateConstrainPage;
        ConclusionPage ConclusionPage = new ConclusionPage();
        PointsPage PointsPage;
        public VendorsAndConsumptionsPage VendorsAndConsumptionsPage;

        private void VendorsAndConsumersRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = VendorsAndConsumptionsPage;
            StageTB.Text = "Этап 1: Добавление участников транспортной задачи";
        }

        private void CarriersRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = NeedServisesAndChoseCarrierPage;
            StageTB.Text = "Этап 4: Выбор перевозчика и оказываемых дополнительных услуг";
        }

        private void TarifsRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            if (VendorsAndConsumptionsPage.IsModificate)
            {
                TarifsAndPointsPage.UpdateMatrix();
                VendorsAndConsumptionsPage.IsModificate = false;
            }
            MainFrame.Content = TarifsAndPointsPage;
            StageTB.Text = "Этап 5: Настройка тарифов перевозок";
        }

        private void ConclusionRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = ConclusionPage;
            StageTB.Text = "Этап 6: Оптимизация и оценка стоимости перевозок";
        }

        private void ConstrainRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = CreateConstrainPage;
            StageTB.Text = "Этап 3: Настройка ограничений";
        }

        private void PointsRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = PointsPage;
            StageTB.Text = "Этап 2: Настройка пунктов снабжения и потребления";
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainForm.Visibility = Visibility.Visible;
            MainForm.RequestPage.UpdateTaskList();
            DBTask = null;
        }
    }
}
