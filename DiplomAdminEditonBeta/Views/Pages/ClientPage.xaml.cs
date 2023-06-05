using DiplomAdminEditonBeta.TCPModels;
using DiplomAdminEditonBeta.Views.Pages;
using Newtonsoft.Json;
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
using System.Windows.Threading;

namespace DiplomAdminEditonBeta
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        public List<Client> Clients = new List<Client>();
        public ClientPage()
        {
            DataContext = this;
            InitializeComponent();
            UpdateClientDatagrid();
        }

        public void UpdateClientDatagrid()
        {
            ClientsDataGrid.ItemsSource = null;

            TCPMessege tCPMessege = new TCPMessege(1,2,null);
            tCPMessege = ClientTCP.SendMessegeAndGetAnswer(tCPMessege);
            if(tCPMessege == null)
            {
                MainForm.ReturnToAutorization();
                return;
            }
            Clients = JsonConvert.DeserializeObject<List<Client>>(tCPMessege.Entity);

            ClientsDataGrid.ItemsSource = Clients;
        }

        public void UpdateClientDatagridFromList()
        {
            ClientsDataGrid.ItemsSource = null;
            ClientsDataGrid.ItemsSource = Clients;
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

                if (MessageBox.Show($"Вы действительно хотите удалить клиента {client.CompanyName}?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }

                TCPMessege tCPMessege = new TCPMessege(5, 2, client.Id);
                ClientTCP.SendMessege(tCPMessege);
                Clients.Remove(client);
                UpdateClientDatagridFromList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainForm.ReturnToAutorization();
                return;
            }
        }

        private DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = new TimeSpan(20) };
        private int position = 0;

        private void ClientsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (ClientsDataGrid.SelectedItem == null)
                    return;
                Client client = ClientsDataGrid.SelectedItem as Client;
                client.Address = client.Address.Trim(' ');
                client.CompanyName = client.CompanyName.Trim(' ');
                client.Email = client.Email.Trim(' ');

                TCPMessege tCPMessege = new TCPMessege(4, 2, client);
                tCPMessege = ClientTCP.SendMessegeAndGetAnswer(tCPMessege);

                if (tCPMessege.CodeOperation > 0)
                {
                    position = Clients.IndexOf(client);
                }
                else
                {
                    List<string> vs = JsonConvert.DeserializeObject<List<string>>(tCPMessege.Entity);
                    MessageBox.Show("При изменении произошла ошибка: " + vs[0]);
                    position = Clients.IndexOf(client);
                    Clients[position] = JsonConvert.DeserializeObject<Client>(vs[1]);
                    UpdateClientDatagridFromList();
                }
                dispatcherTimer.Tick += Timertick;
                dispatcherTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainForm.ReturnToAutorization();
                return;
            }
        }

        private void Timertick(object sender, EventArgs e)
        {
            dispatcherTimer.Stop();
            ClientsDataGrid.SelectedIndex = position;
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchTB.Text == "")
            {
                UpdateClientDatagridFromList();
                return;
            }
            ClientsDataGrid.ItemsSource = null;
            ClientsDataGrid.ItemsSource = Clients.Where(p => p.CompanyName.Contains(SearchTB.Text) || p.Address.Contains(SearchTB.Text) || p.Email.Contains(SearchTB.Text));
        }
    }
}
