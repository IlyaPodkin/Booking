using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для Grades.xaml
    /// </summary>
    public partial class Grades : Page
    {
        public Grades()
        {
            InitializeComponent();
            Loaded += Grade_Loaded;//отработчик события для БД
            DataGridGrades.ItemsSource = db.Grades.Local.ToBindingList();//Вывод записей из бд
        }

        ApplicationContext db = new ApplicationContext();//Подключение БД
        void Grade_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Grades.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Grades.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddGrade addGrade = new AddGrade();
            addGrade.Show();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Grade grade = (Grade)DataGridGrades.SelectedItem;
            // получаем измененный объект
            if (grade != null)
            {
                db.SaveChanges();
            }
            this.NavigationService.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Grade grade = (Grade)DataGridGrades.SelectedItem;
            if (grade == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                try
                {
                    db.Grades.Remove(grade);
                    db.SaveChanges();
                }
                catch
                {
                    db.Update(grade);
                    MessageBox.Show("Невозможно удалить запись, так как эта категория используются в других таблицах!");
                }
            }
        }
    }
}
