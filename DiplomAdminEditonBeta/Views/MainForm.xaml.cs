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
        ClientPage clientPage = new ClientPage();
        public RequestPage RequestPage;
        public static User CurrentUser;
        public static MainForm mainForm;
        public MainForm()
        {
            InitializeComponent();
            mainForm = this;
            RequestPage = new RequestPage(this);
            MainFrame.Content = clientPage;
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
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AutorizationForm autorizationForm = new AutorizationForm();
            autorizationForm.Show();
            Close();
        }

        public static void ReturnToAutorization()
        {
            MessageBox.Show("Соединение с сервером потеряно!");
            AutorizationForm autorizationForm = new AutorizationForm();
            autorizationForm.Show();
            mainForm.Close();
        }
        private void CarriersRB_Checked(object sender, RoutedEventArgs e)
        {
            if (MainFrame == null)
                return;
            MainFrame.Content = CarriersPage;
        }
    }
}
