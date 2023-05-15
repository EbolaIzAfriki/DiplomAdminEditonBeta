using DiplomAdminEditonBeta.Views.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace DiplomAdminEditonBeta
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        CarriersPage CarriersPage;
        public ClientPage(CarriersPage carriersPage)
        {
            DataContext = this;
            InitializeComponent();
            CarriersPage = carriersPage;
            ClientsDataGrid.ItemsSource = DiplomBetaDBEntities.GetContext().Client.ToList();
        }

        public void UpdateClientDatagrid()
        {
            ClientsDataGrid.ItemsSource = null;
            ClientsDataGrid.ItemsSource = DiplomBetaDBEntities.GetContext().Client.ToList();
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            AddClientForm addClientForm = new AddClientForm(this);
            addClientForm.ShowDialog();
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientsDataGrid.SelectedItem == null)
                    return;
                Client client = ClientsDataGrid.SelectedItem as Client;
                DiplomBetaDBEntities.GetContext().Client.Remove(client);
                DiplomBetaDBEntities.GetContext().SaveChanges();
                UpdateClientDatagrid();
                CarriersPage.UpdateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClientsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (ClientsDataGrid.SelectedItem == null)
                    return;
                Client client = ClientsDataGrid.SelectedItem as Client;
                DiplomBetaDBEntities.GetContext().Entry(client).State = EntityState.Modified;
                DiplomBetaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string s = (sender as ComboBox).SelectedItem.ToString();
                if (ClientsDataGrid.SelectedItem == null || s == "")
                    return;
                TypeClient typeClient = DiplomBetaDBEntities.GetContext().TypeClient.Where(p => p.Name == s).FirstOrDefault();
                Client client = ClientsDataGrid.SelectedItem as Client;
                client.TypeClient = typeClient;
                DiplomBetaDBEntities.GetContext().Entry(DiplomBetaDBEntities.GetContext().TypeClient.Where(p => p.Name != s).FirstOrDefault()).State = EntityState.Unchanged;
                DiplomBetaDBEntities.GetContext().SaveChanges();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
