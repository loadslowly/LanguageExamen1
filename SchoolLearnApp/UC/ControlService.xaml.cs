using SchoolLearnApp.Classes;
using SchoolLearnApp.DataBase;
using SchoolLearnApp.UI;
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

namespace SchoolLearnApp.UC
{
    /// <summary>
    /// Логика взаимодействия для ControlService.xaml
    /// </summary>
    public partial class ControlService : UserControl
    {
        private Service service;

        private bool isUser;

        public ControlService(Service service,bool isUser)
        {
            InitializeComponent();

            this.service = service;
            this.isUser = isUser;


            DataContext = service;

            if (isUser)
            {
                BtnDelete.Visibility = Visibility.Hidden;
                BtnEdit.Visibility = Visibility.Hidden;
            }

            if (service.Discount == 0)
            {
                Cost.Text = $"{Convert.ToInt64(service.Cost)} рублей за {service.DurationInMinutes} минут";
                Discount.Visibility = Visibility.Hidden;
            }
            else
            {
                Cost.Text = $"{Convert.ToInt64(service.Cost - (service.Cost * Convert.ToDecimal(service.Discount)))} рублей за {service.DurationInMinutes} минут";
                Discount.Text = $"* скидка {service.Discount * 100}%";
            }

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEditService(service, isUser));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить услугу?", "Подтверждение",MessageBoxButton.YesNoCancel,MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    BaseChanger.DeleteService(service.ID);
                    MessageBox.Show("Услуга была успешно удаленна!","Успешно",MessageBoxButton.OK,MessageBoxImage.Information);
                    Manager.MainFrame.Navigate(new PageService(isUser));
                }
                catch
                {
                    MessageBox.Show("Удаление не было произведенно!");
                }
            }
        }
    }
}
