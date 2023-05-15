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

namespace DiplomAdminEditonBeta.Views.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUserForm.xaml
    /// </summary>
    public partial class AddCarrierForm : Window
    {
        CarriersPage CarriersPage;
        public AddCarrierForm(CarriersPage carriersPage)
        {
            InitializeComponent();
            CarriersPage = carriersPage;
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DiplomBetaDBEntities.GetContext().Carrier.Add(new Carrier()
                {
                    Address = AddressTB.Text,
                    Email = EmailTB.Text,
                    Name = NameTB.Text,
                    Phone = PhoneTB.Text
                });
                DiplomBetaDBEntities.GetContext().SaveChanges();
                CarriersPage.UpdateDataGrid();
                MessageBox.Show("Перевозчик был добавлен!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
