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
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;


namespace Администратирование_предприятия.View
{
    /// <summary>
    /// Логика взаимодействия для TimeBoxs.xaml
    /// </summary>
    public partial class TimeBoxs : Page
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void TimeBox_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.TimeBoxs.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.TimeBoxs.Local.ToObservableCollection();
        }
        public TimeBoxs()
        {
            InitializeComponent();
            Loaded += TimeBox_Loaded;//отработчик события для БД
            DataGridTimeBoxs.ItemsSource = db.TimeBoxs.Local.ToBindingList();//Вывод записей из бд
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            TimeBox timeBox = (TimeBox)DataGridTimeBoxs.SelectedItem;
            // получаем измененный объект
            if (timeBox != null)
            {
                db.SaveChanges();
            }
            this.NavigationService.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            TimeBox timeBox = (TimeBox)DataGridTimeBoxs.SelectedItem;
            if (timeBox == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                try
                {
                    db.TimeBoxs.Remove(timeBox);
                    db.SaveChanges();
                }
                catch
                {
                    db.Update(timeBox);
                    MessageBox.Show("Невозможно удалить запись, так как эта категория используются в других таблицах!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddTimeBox addTimeBox = new AddTimeBox();
            addTimeBox.Show();
        }
    }
}
