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

namespace DiplomAdminEditonBeta.Views.PagesTask
{
    /// <summary>
    /// Логика взаимодействия для PointsPage.xaml
    /// </summary>
    public partial class PointsPage : Page
    {
        MainWorkOnTaskForm MainWorkOnTaskForm;
        public PointsPage(MainWorkOnTaskForm mainWorkOnTaskForm)
        {
            InitializeComponent();
            MainWorkOnTaskForm = mainWorkOnTaskForm;
        }

       public void UpdateDataGrid()
        {
            PointsDataGrid.ItemsSource = null;
            PointsDataGrid.ItemsSource = MainWorkOnTaskForm.DBTask.Point.ToList();
        }

        private void PointsDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Point point = PointsDataGrid.SelectedItem as Point;
            if (point == null)
                return;
            DiplomBetaDBEntities.GetContext().Entry(point).State = EntityState.Modified;
            DiplomBetaDBEntities.GetContext().SaveChanges();

            if (!MainWorkOnTaskForm.DBTask.Point.Select(p => p.ProductCount).Contains(0))
            {
                MainWorkOnTaskForm.TarifsRB.IsEnabled = true;
            }
            else
            {
                MainWorkOnTaskForm.TarifsRB.IsEnabled = false;
            }

            VendorsAndConsumptionsPage.IsModificate = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        
        {
            try
            {
                if (!(sender as ComboBox).IsMouseOver || PointsDataGrid.SelectedItem == null)
                    return;
                string s = (sender as ComboBox).SelectedItem.ToString();
                Point point = PointsDataGrid.SelectedItem as Point;
                DiplomBetaDBEntities.GetContext().Entry(DiplomBetaDBEntities.GetContext().Client.First(p => p.Id == point.IdClient)).State = EntityState.Unchanged;
                point.Client = DiplomBetaDBEntities.GetContext().Client.FirstOrDefault(p => p.CompanyName == s);
                DiplomBetaDBEntities.GetContext().Entry(point).State = EntityState.Modified;
                DiplomBetaDBEntities.GetContext().SaveChanges();

                UpdateDataGrid();
                MainWorkOnTaskForm.VendorsAndConsumptionsPage.UpdateDataGridConsumptions();
                MainWorkOnTaskForm.VendorsAndConsumptionsPage.UpdateDataGridVendors();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
