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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class CarriersPage : Page
    {
        private List<Service> Services, AllServices;
        public List<Carrier> Carriers;
        public CarriersPage()
        {
            InitializeComponent();

            TCPMessege tCPMessege = new TCPMessege(1, 35, null);
            tCPMessege = ClientTCP.SendMessegeAndGetAnswer(tCPMessege);
            if (tCPMessege == null)
            {
                MainForm.ReturnToAutorization();
                return;
            }
            List<string> recivedStrings = JsonConvert.DeserializeObject<List<string>>(tCPMessege.Entity);

            Services = JsonConvert.DeserializeObject<List<Service>>(recivedStrings[0]);
            AllServices = JsonConvert.DeserializeObject<List<Service>>(recivedStrings[0]);
            Carriers = JsonConvert.DeserializeObject<List<Carrier>>(recivedStrings[1]);

            CarriersDataGrid.ItemsSource = Carriers;
        }

        public void UpdateCarriersDatagridFromList()
        {
            CarriersDataGrid.ItemsSource = null;
            CarriersDataGrid.ItemsSource = Carriers;
        }

        public void UpdateDataGrid()
        {
            CarriersDataGrid.ItemsSource = null;

            TCPMessege tCPMessege = new TCPMessege(1, 3, null);
            tCPMessege = ClientTCP.SendMessegeAndGetAnswer(tCPMessege);
            if (tCPMessege == null)
            {
                MainForm.ReturnToAutorization();
                return;
            }
            Carriers = JsonConvert.DeserializeObject<List<Carrier>>(tCPMessege.Entity);

            CarriersDataGrid.ItemsSource = Carriers;
        }

        public void UpdateServicesDataGrids()
        {
            if (CurrentCarrier == null)
                return;
            if (CurrentCarrier == null)
                return;
            ServiceDataGrid.ItemsSource = null;
            ServiceDataGrid.ItemsSource = CurrentCarrier.ServiceCarrier.ToList();
            StaticServiceDataGrid.ItemsSource = null;
            StaticServiceDataGrid.ItemsSource = Services.Where(p => !CurrentCarrier.ServiceCarrier.Select(n => n.Service.Id).Contains(p.Id)).ToList();
        }
        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            AddCarrierForm addCarrierForm = new AddCarrierForm(this);
            addCarrierForm.ShowDialog();
        }

        private void DeleteCarriersBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null)
                    return;
                Carrier carrier = CarriersDataGrid.SelectedItem as Carrier;
                if (MessageBox.Show($"Вы действительно хотите удалить перевозчика {carrier.Name}?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }


                TCPMessege tCPMessege = new TCPMessege(5, 3, carrier);
                ClientTCP.SendMessege(tCPMessege);

                Carriers.Remove(carrier);
                UpdateCarriersDatagridFromList();
                ClearServiseDG();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainForm.ReturnToAutorization();
                return;
            }
        }

        private void ClearServiseDG()
        {
            Services = AllServices.ToList();
            StaticServiceDataGrid.ItemsSource = null;
            StaticServiceDataGrid.ItemsSource = Services;
            ServiceDataGrid.ItemsSource = null;
        }

        private DispatcherTimer dispatcherTimer = new DispatcherTimer() { Interval = new TimeSpan(20) };
        private int position = 0;
        private void CarriersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null)
                    return;
                Carrier carrier = CarriersDataGrid.SelectedItem as Carrier;
                carrier.Name = carrier.Name.Trim();
                carrier.Address = carrier.Address.Trim();
                carrier.Email = carrier.Email.Trim();

                TCPMessege tCPMessege = new TCPMessege(4, 3, carrier);
                tCPMessege = ClientTCP.SendMessegeAndGetAnswer(tCPMessege);

                if (tCPMessege.CodeOperation > 0)
                {
                    position = Carriers.IndexOf(carrier);
                }
                else
                {
                    List<string> vs = JsonConvert.DeserializeObject<List<string>>(tCPMessege.Entity);
                    MessageBox.Show("При изменении произошла ошибка: " + vs[0]);
                    position = Carriers.IndexOf(carrier);
                    Carriers[position] = JsonConvert.DeserializeObject<Carrier>(vs[1]);
                    UpdateCarriersDatagridFromList();
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
            CarriersDataGrid.SelectedIndex = position;
        }

        private void CarriersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null)
                {
                    ClearServiseDG();
                    return;
                }
                Carrier carrier = CarriersDataGrid.SelectedItem as Carrier;
                ServiceDataGrid.ItemsSource = carrier.ServiceCarrier.ToList();
                StaticServiceDataGrid.ItemsSource = Services.Where(p => !carrier.ServiceCarrier.Select(n => n.IdService).Contains(p.Id)).ToList();
                CurrentCarrier = carrier;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Carrier CurrentCarrier;
        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null || StaticServiceDataGrid.SelectedItem == null)
                    return;
                Service service = StaticServiceDataGrid.SelectedItem as Service;
                Carrier carrier = (Carrier)CarriersDataGrid.SelectedItem;

                TCPMessege tCPMessege = new TCPMessege(3, 35, new List<int>() { service.Id, carrier.Id });
                ClientTCP.SendMessege(tCPMessege);
                CurrentCarrier.ServiceCarrier.Add(new ServiceCarrier() { Carrier = CurrentCarrier, Cost = 0, Service = service });

                UpdateServicesDataGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainForm.ReturnToAutorization();
                return;
            }
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null || ServiceDataGrid.SelectedItem == null)
                    return;
                ServiceCarrier serviceCarrier = ServiceDataGrid.SelectedItem as ServiceCarrier;

                TCPMessege tCPMessege = new TCPMessege(5, 35, new List<int> { serviceCarrier.Service.Id, serviceCarrier.Carrier.Id });
                ClientTCP.SendMessege(tCPMessege);

                CurrentCarrier.ServiceCarrier.Remove(serviceCarrier);
                UpdateServicesDataGrids();
            }
            catch
            {
                MainForm.ReturnToAutorization();
                return;
            }
           
        }

        private int lastCost;
        private void ServiceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ServiceCarrier serviceCarrier = ServiceDataGrid.SelectedItem as ServiceCarrier;
            if (serviceCarrier == null)
                return;
            lastCost = serviceCarrier.Cost;
        }

        private void MaskedTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            CarriersDataGrid_CellEditEnding(sender, null);
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchTB.Text == "")
            {
                UpdateCarriersDatagridFromList();
                return;
            }
            CarriersDataGrid.ItemsSource = null;
            CarriersDataGrid.ItemsSource = Carriers.Where(p => p.Name.Contains(SearchTB.Text) || p.Address.Contains(SearchTB.Text) || p.Email.Contains(SearchTB.Text) || p.Phone.Contains(SearchTB.Text));
        }

        private void ServiceDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (ServiceDataGrid.SelectedItem == null)
                    return;
                ServiceCarrier serviceCarrier = ServiceDataGrid.SelectedItem as ServiceCarrier;
                if (lastCost == serviceCarrier.Cost)
                    return;
                TCPMessege tCPMessege = new TCPMessege(4, 35, new List<int> { serviceCarrier.Service.Id, serviceCarrier.Carrier.Id, serviceCarrier.Cost });
                ClientTCP.SendMessege(tCPMessege);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MainForm.ReturnToAutorization();
                return;
            }
        }
    }
}
