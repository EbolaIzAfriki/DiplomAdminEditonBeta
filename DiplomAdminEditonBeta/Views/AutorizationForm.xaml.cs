using DiplomAdminEditonBeta.TCPModels;
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

namespace DiplomAdminEditonBeta
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AutorizationForm : Window
    {
        public AutorizationForm()
        {
            InitializeComponent();
        }

        private void EntryButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ClientTCP.IsConnected)
                {
                    ClientTCP.Connect();
                    ClientTCP.IsConnected = true;
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            TCPMessege tCPMessege = new TCPMessege(2, 1, new User() { Login = LoginTB.Text, Password = PasswordTB.Password });
            tCPMessege = ClientTCP.SendMessegeAndGetAnswer(tCPMessege);
            if(tCPMessege == null)
            {
                return;
            }
            if(tCPMessege.CodeOperation == 0)
            {
                MessageBox.Show("Неверно введен логин или пароль!");
                return;
            }
            MainForm mainForm = new MainForm();
            MainForm.CurrentUser = JsonConvert.DeserializeObject<User>(tCPMessege.Entity);
            mainForm.Show();
            Close();
        }
    }
}
