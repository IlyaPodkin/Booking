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
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;

namespace Администратирование_предприятия.View
{
    /// <summary>
    /// Логика взаимодействия для ShowUsers.xaml
    /// </summary>
    public partial class ShowUsers : Window
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД

        public ShowUsers()
        {
            InitializeComponent();
            Loaded += User_Loaded;//отработчик события для БД
            DataGridUsers.ItemsSource = db.Users.Local.ToBindingList();//Вывод записей из бд
        }
        void User_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            User user = (User)DataGridUsers.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            User user = (User)DataGridUsers.SelectedItem;
            // получаем измененный объект
            if (user != null)
            {
                db.SaveChanges();
                DataGridUsers.Items.Refresh();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void BackButton(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Hide();
        }
    }
}
