using DiplomAdminEditonBeta.Views;
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

namespace DiplomAdminEditonBeta
{
    /// <summary>
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        MainForm MainForm;
        public RequestPage(MainForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
        }

        private void AddTaskBut_Click(object sender, RoutedEventArgs e)
        {
            MainWorkOnTaskForm mainWorkOnTaskForm = new MainWorkOnTaskForm(MainForm);
            mainWorkOnTaskForm.Show();
            MainForm.Visibility = Visibility.Hidden;
        }

        public void UpdateTaskList()
        {
            ClientsDataGrid.ItemsSource = null;
            ClientsDataGrid.ItemsSource = DiplomBetaDBEntities.GetContext().Task.Where(p => p.UserId == MainForm.CurrentUser.Id || p.UserId == null).ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить задачу?", "Удаление задачи", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                if (ClientsDataGrid.SelectedItem == null)
                    return;
                DiplomBetaDBEntities.GetContext().Task.Remove(ClientsDataGrid.SelectedItem as Task);
                DiplomBetaDBEntities.GetContext().SaveChanges();
                UpdateTaskList();
            }
        }

        private void ClientsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ClientsDataGrid.SelectedItem == null)
                return;
            MainWorkOnTaskForm mainWorkOnTaskForm = new MainWorkOnTaskForm(MainForm, ClientsDataGrid.SelectedItem as Task);
            mainWorkOnTaskForm.Show();
            MainForm.Visibility = Visibility.Hidden;
        }
    }
}
