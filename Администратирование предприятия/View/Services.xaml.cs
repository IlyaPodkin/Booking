using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xaml;
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;


namespace Администратирование_предприятия
{
    /// <summary>
    /// Логика взаимодействия для Services.xaml
    /// </summary>
    public partial class Services : Page
    {
        public Services()
        {
            InitializeComponent();
            Loaded += Service_Loaded;//отработчик события для БД
            DataGridServices.ItemsSource = db.Services.Local.ToBindingList();//Вывод записей из бд

        }

        ApplicationContext db = new ApplicationContext();//Подключение БД
        public Service? Servise { get; private set; }
        void Service_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            // загружаем данные из БД
            db.Services.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Services.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddService addService = new AddService();
            addService.Show();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Service service = (Service)DataGridServices.SelectedItem;
            // получаем измененный объект
            if (service != null)
            {
                db.SaveChanges();
            }
            this.NavigationService.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Service service = (Service)DataGridServices.SelectedItem;
            if (service == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                try
                {
                    db.Services.Remove(service);
                    db.SaveChanges();
                }
                catch
                {
                    db.Update(service);
                    MessageBox.Show("Невозможно удалить запись, так как эта категория используются в других таблицах!");
                }

            }
        }
    }
}
