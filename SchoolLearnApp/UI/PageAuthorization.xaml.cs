using SchoolLearnApp.Classes;
using SchoolLearnApp.DataBase;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SchoolLearnApp.UI
{
    /// <summary>
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        public PageAuthorization()
        {
            InitializeComponent();
        }

        private void UserCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (UserCheckBox.IsChecked.Value)
            {
                UserPasswordBox.Visibility = Visibility.Hidden;
                UserPassword.Text = UserPasswordBox.Password;
                UserPassword.Visibility = Visibility.Visible;
            }
            else
            {
                UserPassword.Visibility = Visibility.Hidden;
                UserPasswordBox.Password = UserPassword.Text;
                UserPasswordBox.Visibility = Visibility.Visible;
            }
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var data = SchoolLanguageEntities.GetContext();

            var getUsers = from client in data.Client
                           where client.Login == UserLogin.Text && (client.Password == UserPasswordBox.Password || client.Password == UserPassword.Text)
                           select client;

            if (getUsers == null || !getUsers.Any())
            {
                var getAdmin = from worker in data.Worker
                               where worker.Login == UserLogin.Text && (worker.Password == UserPasswordBox.Password || worker.Password == UserPassword.Text)
                               select worker;

                if (getAdmin != null || getAdmin.Any())
                {
                    MessageBox.Show("Добро пожаловать, администратор!");
                    Manager.MainFrame.Navigate(new PageService(false));
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!");
                }            
            }
            else
            {
                MessageBox.Show($"Добро пожаловать, пользователь {UserLogin.Text}!");
                Manager.MainFrame.Navigate(new PageService(true));
            }
        }
    }
}
