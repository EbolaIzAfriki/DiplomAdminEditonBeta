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
    /// Логика взаимодействия для NeedServisesAndChoseCarrierPage.xaml
    /// </summary>
    public partial class NeedServisesAndChoseCarrierPage : Page
    {
        List<Service> ChosedService = new List<Service>(), NotChosedService;

        private void AddServiceButton_Click(object sender, RoutedEventArgs e)
        {
           /* if (ListServiceDataGrid.SelectedItem == null)
                return;
            ChosedService.Add(ListServiceDataGrid.SelectedItem as Service);
            NotChosedService.Remove(ListServiceDataGrid.SelectedItem as Service);

            MainWorkOnTaskForm.DBTask.Service.Add(ListServiceDataGrid.SelectedItem as Service);
            DiplomBetaDBEntities.GetContext().SaveChanges();

            UpdateDatagrids();*/
        }

        public NeedServisesAndChoseCarrierPage()
        {
            InitializeComponent();
            /*NotChosedService = DiplomBetaDBEntities.GetContext().Service.ToList();
            ListServiceDataGrid.ItemsSource = NotChosedService;
            if (MainWorkOnTaskForm.DBTask == null)
                return;
            if (MainWorkOnTaskForm.DBTask.Service != null)
            {
                ChosedService = MainWorkOnTaskForm.DBTask.Service.ToList();
                foreach(Service service in ChosedService)
                {
                    if (NotChosedService.Contains(service))
                    {
                        NotChosedService.Remove(service);
                    }
                }
            }
            if(MainWorkOnTaskForm.DBTask.Carrier!= null)
            {
                CurrentCarrierTB.Text = "Текущий поставщик: " + MainWorkOnTaskForm.DBTask.Carrier.Name;
            }
            ChoseServiceDataGrid.ItemsSource = ChosedService;*/
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            /*if (ChoseServiceDataGrid.SelectedItem == null)
                return;
            NotChosedService.Add(ChoseServiceDataGrid.SelectedItem as Service);
            ChosedService.Remove(ChoseServiceDataGrid.SelectedItem as Service);

            MainWorkOnTaskForm.DBTask.Service.Remove(ChoseServiceDataGrid.SelectedItem as Service);
            DiplomBetaDBEntities.GetContext().SaveChanges();

            UpdateDatagrids();*/


        }

        private void CarriersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           /* if (CarriersDataGrid.SelectedItem == null)
                return;
            Carrier carrier = CarriersDataGrid.SelectedItem as Carrier;
            MainWorkOnTaskForm.DBTask.Carrier = carrier;
            CurrentCarrierTB.Text = "Текущий поставщик: " + carrier.Name;
            DiplomBetaDBEntities.GetContext().SaveChanges();*/
        }

        public void UpdateDatagrids()
        {
            /*ChoseServiceDataGrid.ItemsSource = null;
            ListServiceDataGrid.ItemsSource = null;
            CarriersDataGrid.ItemsSource = null;
            ChoseServiceDataGrid.ItemsSource = ChosedService;
            ListServiceDataGrid.ItemsSource = NotChosedService;

            List<Carrier> carriers = new List<Carrier>();
            foreach (Carrier carrier in DiplomBetaDBEntities.GetContext().Carrier.ToList())
            {
                bool NotContains = false;
                List<Service> services = carrier.ServiceCarrier.Select(p => p.Service).ToList();
                foreach (Service service in ChosedService)
                {
                    if (!services.Contains(service))
                    {
                        NotContains = true;
                        break;
                    }
                }
                if (NotContains)
                    continue;
                carriers.Add(carrier);
            }
            CarriersDataGrid.ItemsSource = carriers;*/
        }
    }
}
