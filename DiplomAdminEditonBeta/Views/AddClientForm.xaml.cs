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
    /// Логика взаимодействия для AddClientForm.xaml
    /// </summary>
    public partial class AddClientForm : Window
    {
        ClientPage ClientPage;
        public AddClientForm(ClientPage clientPage)
        {
            InitializeComponent();
            ClientPage = clientPage;
        }
        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Client client = new Client() { Address = AddressTB.Text, CompanyName = CompanyNameTB.Text, Email = EmailTB.Text};

                if (CompanyTypeCB.SelectedIndex == 0)
                    client.TypeClient = DiplomBetaDBEntities.GetContext().TypeClient.ToList().FirstOrDefault();
                else
                {
                    client.TypeClient = DiplomBetaDBEntities.GetContext().TypeClient.ToList().LastOrDefault();
                }

                DiplomBetaDBEntities.GetContext().Client.Add(client);
                DiplomBetaDBEntities.GetContext().SaveChanges();
                ClientPage.UpdateClientDatagrid();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
