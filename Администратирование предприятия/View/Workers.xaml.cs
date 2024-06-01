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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;


namespace Администратирование_предприятия
{
    /// <summary>
    /// Логика взаимодействия для Workers.xaml
    /// </summary>
    public partial class Workers : Page
    {
        public Workers()
        {
            InitializeComponent();
            Loaded += Worker_Loaded;//отработчик события для БД
            DataGridServices.ItemsSource = db.Workers.Local.ToBindingList();//Вывод записей из бд
        }
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void Worker_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Workers.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Workers.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddWorker addWorker = new AddWorker();
            addWorker.Show();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Worker worker = (Worker)DataGridServices.SelectedItem;
            // получаем измененный объект
            if (worker != null)
            {
                db.SaveChanges();
            }
            this.NavigationService.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Worker worker = (Worker)DataGridServices.SelectedItem;
            if (worker == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                try
                {
                    db.Workers.Remove(worker);
                    db.SaveChanges();
                }
                catch
                {
                    db.Update(worker);
                    MessageBox.Show("Невозможно удалить запись, так как эта категория используются в других таблицах!");
                }
            }
        }
    }
}
