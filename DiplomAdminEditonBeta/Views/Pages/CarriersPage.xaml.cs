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
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class CarriersPage : Page
    {
        private List<Service> Services;
        public CarriersPage()
        {
            InitializeComponent();
            Services = DiplomBetaDBEntities.GetContext().Service.ToList();
            CarriersDataGrid.ItemsSource = DiplomBetaDBEntities.GetContext().Carrier.ToList();
        }

        public void UpdateDataGrid()
        {
            CarriersDataGrid.ItemsSource = null;
            CarriersDataGrid.ItemsSource = DiplomBetaDBEntities.GetContext().Carrier.ToList();
        }

        public void UpdateServicesDataGrids()
        {
            if (CurrentCarrier == null)
                return;
            CurrentCarrier = DiplomBetaDBEntities.GetContext().Carrier.FirstOrDefault(p => p.Id == CurrentCarrier.Id);
            if (CurrentCarrier == null)
                return;
            ServiceDataGrid.ItemsSource = null;
            ServiceDataGrid.ItemsSource = CurrentCarrier.ServiceCarrier.ToList();
            StaticServiceDataGrid.ItemsSource = null;
            StaticServiceDataGrid.ItemsSource = Services.Where(p => !CurrentCarrier.ServiceCarrier.Select(n => n.IdService).Contains(p.Id)).ToList();
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
                DiplomBetaDBEntities.GetContext().Carrier.Remove(carrier);
                DiplomBetaDBEntities.GetContext().SaveChanges();
                UpdateDataGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarriersDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null)
                    return;
                Carrier carrier = CarriersDataGrid.SelectedItem as Carrier;
                DiplomBetaDBEntities.GetContext().Entry(carrier).State = EntityState.Modified;
                DiplomBetaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CarriersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (CarriersDataGrid.SelectedItem == null)
                    return;
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
                DiplomBetaDBEntities.GetContext().ServiceCarrier.Add(new ServiceCarrier() { Carrier = (Carrier)CarriersDataGrid.SelectedItem, Service = service, Cost = 0 });
                DiplomBetaDBEntities.GetContext().SaveChanges();
                UpdateServicesDataGrids();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (CarriersDataGrid.SelectedItem == null || ServiceDataGrid.SelectedItem == null)
                return;
            ServiceCarrier serviceCarrier = ServiceDataGrid.SelectedItem as ServiceCarrier;
            DiplomBetaDBEntities.GetContext().ServiceCarrier.Remove(serviceCarrier);
            DiplomBetaDBEntities.GetContext().SaveChanges();
            UpdateServicesDataGrids();
        }

        private void ServiceDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                if (ServiceDataGrid.SelectedItem == null)
                    return;
                ServiceCarrier serviceCarrier = ServiceDataGrid.SelectedItem as ServiceCarrier;
                DiplomBetaDBEntities.GetContext().Entry(serviceCarrier).State = EntityState.Modified;
                DiplomBetaDBEntities.GetContext().SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
