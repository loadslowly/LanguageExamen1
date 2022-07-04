using Microsoft.Win32;
using SchoolLearnApp.Classes;
using SchoolLearnApp.DataBase;
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

namespace SchoolLearnApp.UI
{
    /// <summary>
    /// Логика взаимодействия для PageAddEditService.xaml
    /// </summary>
    public partial class PageAddEditService : Page
    {
        private Service service = new Service();

        private bool isUser;

        public PageAddEditService(Service service,bool isUser)
        {
            InitializeComponent();

            if (service != null)
            {
                this.service = service;
            }

            DataContext = this.service;
        }

        private void BtnPickImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            
            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.FileName.EndsWith(".jpg") || openFileDialog.FileName.EndsWith(".png"))
                {
                    service.MainImagePath = openFileDialog.FileName;
                    ImagePreview.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
                else
                {
                    MessageBox.Show("Ошибка неверный формат!");
                }

            }         
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {

            var data = SchoolLanguageEntities.GetContext();

            StringBuilder error = new StringBuilder();

            if (Title.Text.Length == 0)
                error.AppendLine("Пожалуйста, заполните название услуги!");
            if (Cost.Text.Length == 0 || service.Cost < 0)
                error.AppendLine("Услуга не может быть бесплатной!");
            if (DurationInMinutes.Text.Length == 0)
                error.AppendLine("Пожалуйста, напишите длительность услуги!");
            if (service.DurationInMinutes > 240)
                error.AppendLine("Услуга не может быть дольше 4 часов");
            if (service.Discount < 0)
                error.AppendLine("Скидка не может быть отрицательной!");
            if (service.Discount > 1)
                error.AppendLine("Скидка не может быть больше 100%!");


            if (service.ID == 0)
            {
                foreach (Service service in data.Service)
                {
                    if (service.Title == Title.Text && service.Title.Length == Title.Text.Length)
                    {
                        error.AppendLine("Такое название уже сущетсвует!");
                    }
                }
            }

            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }

            if (service.ID == 0)
            {
                SchoolLanguageEntities.GetContext().Service.Add(service);

            }


            try
            {
                SchoolLanguageEntities.GetContext().SaveChanges();
                MessageBox.Show("Услуга успешно добавленна!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.MainFrame.Navigate(new PageService(isUser));

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                SchoolLanguageEntities.GetContext().Service.Remove(service);
            }

        }
    }
}
