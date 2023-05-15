using DiplomAdminEditonBeta.Views.Pages;
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

namespace DiplomAdminEditonBeta
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {

        CarriersPage CarriersPage = new CarriersPage();
        ClientPage clientPage;
        public RequestPage RequestPage;
        public static User CurrentUser;
        public MainForm()
        {
            InitializeComponent();
            RequestPage = new RequestPage(this);
            clientPage = new ClientPage(CarriersPage);
            MainFrame.Content = clientPage;
            CurrentUser = DiplomBetaDBEntities.GetContext().User.First();
        }

        private void ClientsRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = clientPage;
        }

        private void RequestsRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = RequestPage;
            RequestPage.UpdateTaskList();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutorizationForm autorizationForm = new AutorizationForm();
            autorizationForm.ShowDialog();
        }

        private void Window_StylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {

        }

        private void CarriersRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = CarriersPage;
        }
    }
}
