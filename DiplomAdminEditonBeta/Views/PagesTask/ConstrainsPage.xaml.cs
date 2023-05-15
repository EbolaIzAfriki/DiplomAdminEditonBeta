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
    /// Логика взаимодействия для ConstrainsPage.xaml
    /// </summary>
    public partial class ConstrainsPage : Page
    {
        public ConstrainsPage()
        {
            InitializeComponent();
            UpdateList();
        }

        private void ButtonAddConstrain_Click(object sender, RoutedEventArgs e)
        {
            Constraint сonstraint = new Constraint() { Task = MainWorkOnTaskForm.DBTask, TypeConstraint = DiplomBetaDBEntities.GetContext().TypeConstraint.First(), ProductCount = 0};
            MainWorkOnTaskForm.DBTask.Constraint.Add(сonstraint);
            UpdateList();
            DiplomBetaDBEntities.GetContext().Constraint.Add(сonstraint);
            DiplomBetaDBEntities.GetContext().SaveChanges();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;
            DiplomBetaDBEntities.GetContext().Constraint.Remove((Constraint)cmd.DataContext);
            DiplomBetaDBEntities.GetContext().SaveChanges();
            UpdateList();
        }

        private void UpdateList()
        {
            ListBoxConstrain.ItemsSource = null;
            ListBoxConstrain.ItemsSource = MainWorkOnTaskForm.DBTask.Constraint.ToList();
        }
        private void ComboBoxVendors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender as ComboBox).IsMouseOver || (sender as ComboBox).SelectedItem == null)
                return;

            Constraint constraint = (Constraint)(sender as ComboBox).DataContext;
            if(constraint.IdPoints == null)
            {
                constraint.IdPoints = ((sender as ComboBox).SelectedItem as Point).Position + "&" + "-1";
            }
            else
            {
                List<string> ListPoints = constraint.IdPoints.Split('&').ToList();
                ListPoints[0] = ((sender as ComboBox).SelectedItem as Point).Position.ToString();
                constraint.IdPoints = ListPoints[0] + "&" + ListPoints[1];
            }
            DiplomBetaDBEntities.GetContext().SaveChanges();
        }

        private void ComboBoxConsumers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender as ComboBox).IsMouseOver || (sender as ComboBox).SelectedItem == null)
                return;

            Constraint constraint = (Constraint)(sender as ComboBox).DataContext;
            if (constraint.IdPoints == null)
            {
                constraint.IdPoints = "-1" + "&" + ((sender as ComboBox).SelectedItem as Point).Position;
            }
            else
            {
                List<string> ListPoints = constraint.IdPoints.Split('&').ToList();
                ListPoints[1] = ((sender as ComboBox).SelectedItem as Point).Position.ToString();
                constraint.IdPoints = ListPoints[0] + "&" + ListPoints[1];
            }
            DiplomBetaDBEntities.GetContext().SaveChanges();
        }

        private void ComboBoxConstrain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender as ComboBox).IsMouseOver || (sender as ComboBox).SelectedItem == null)
                return;

            Constraint constraint = (Constraint)(sender as ComboBox).DataContext;
            constraint.TypeConstraint = (sender as ComboBox).SelectedItem as TypeConstraint;
            DiplomBetaDBEntities.GetContext().SaveChanges();
        }

        private void TextBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            Constraint constraint = (Constraint)textBox.DataContext;
            constraint.ProductCount = int.Parse(textBox.Text);
            DiplomBetaDBEntities.GetContext().SaveChanges();
        }
    }
}
