using SchoolLearnApp.Classes;
using SchoolLearnApp.DataBase;
using SchoolLearnApp.UC;
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
    /// Логика взаимодействия для PageService.xaml
    /// </summary>
    public partial class PageService : Page
    {
        private bool isUser;

        public PageService(bool isUser)
        {
            InitializeComponent();

            this.isUser = isUser;

            if (isUser)
                BtnAdd.Visibility = Visibility.Hidden;

            var data = SchoolLanguageEntities.GetContext();

            foreach(Service service in data.Service)
            {
                UCList.Items.Add(new ControlService(service, isUser));
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var data = SchoolLanguageEntities.GetContext();

            var selectService = from service in data.Service
                                where service.Title.StartsWith(TBSearch.Text)
                                select service;

            UCList.Items.Clear();

            foreach (Service service in selectService)
            {
                UCList.Items.Add(new ControlService(service, isUser));
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            var data = SchoolLanguageEntities.GetContext();

            UCList.Items.Clear();
            TBSearch.Text = "";

            foreach (Service service in data.Service)
            {
                UCList.Items.Add(new ControlService(service, isUser));
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var data = SchoolLanguageEntities.GetContext();

            if (CheckBoxSort.IsChecked.Value)
            {

                var selectService = from service in data.Service
                                    orderby service.Title
                                    select service;

                UCList.Items.Clear();

                foreach (Service service in selectService)
                {
                    UCList.Items.Add(new ControlService(service, isUser));
                }
            }
            else
            {
                UCList.Items.Clear();

                foreach (Service service in data.Service)
                {
                    UCList.Items.Add(new ControlService(service, isUser));
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new PageAddEditService(null,isUser));
        }
    }
}
