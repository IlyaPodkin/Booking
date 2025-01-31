using System;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Логика взаимодействия для AddGrade.xaml
    /// </summary>
    public partial class AddGrade : Window
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void Grade_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Grades.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Grades.Local.ToObservableCollection();
        }

        public AddGrade()
        {
            InitializeComponent();
            Loaded += Grade_Loaded;//отработчик события для БД
        }

        private void ButtonEntryRegClick(object sender, RoutedEventArgs e)
        {
            Guid ID = Guid.NewGuid();//генерация гуида(ID) в бд
            string NameGrade = InputNameGrade.Text.Trim();// получаем вводимый текст в поле наименование услуги


            if (NameGrade.Length == 0 || InputCoefficient.Text.Length == 0) //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Заполните пустые поля!");
            }
            else
            {
                if (float.TryParse(InputCoefficient.Text, out _))
                {
                    float coefficient = Convert.ToSingle(InputCoefficient.Text.Trim());

                    Grade Grade = new Grade(ID, NameGrade, coefficient);
                    db.Grades.Add(Grade);
                    //Добавление и сохранение данных в таблицу Service
                    db.SaveChanges();
                    Grades grade = new Grades();
                    grade.DataGridGrades.ItemsSource = db.Grades.Local.ToBindingList();
                    grade.Refresh();
                    MessageBox.Show("Категория добавлена!");
                }
                else
                {
                    MessageBox.Show("Некоретный формат введенных данных");
                }
            }

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
