using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Администратирование_предприятия.Models;
using Администратирование_предприятия.View;

namespace Администратирование_предприятия
{
    /// <summary>
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Hide();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_Notifications(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Notifications();
        }

        private void Button_Click_BadgeAccountHorizonta(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Bookings();
        }

        private void Button_Click_Workers(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Workers();
        }

        private void Button_Click_Services(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Services();
        }

        private void Button_Click_Grades(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Grades();
        }        
        private void Button_Click_Products(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new Products();
        }
        private void ButtonClickTime(object sender, RoutedEventArgs e)
        {
            MyFrame.Content = new TimeBoxs();
        }

    private void ButtonInfoClick(object sender, RoutedEventArgs e)
        {
            string commandText = "Справка.pdf";
            var proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}
