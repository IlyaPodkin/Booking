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


namespace Администратирование_предприятия
{
    /// <summary>
    /// Логика взаимодействия для AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void AddWorker_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Workers.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Workers.Local.ToObservableCollection();
        }

        public AddWorker()
        {
            InitializeComponent();
            Loaded += AddWorker_Loaded;//отработчик события для БД
            BindItemInComboBox();
        }

        public List<Grade>? grades { get; set; }

        private void BindItemInComboBox()
        {
            var grade = db.Grades.ToList();
            grades = grade;
            InputNameGrade.ItemsSource = grades;
        }

        private void ButtonEntryRegClick(object sender, RoutedEventArgs e)
        {
            Guid ID = Guid.NewGuid();//генерация гуида(ID) в бд
            string NameWorker = InputNameWorker.Text.Trim();// получаем вводимый текст в поле наименование услуги
            string gradeId = InputNameGrade.Text.Trim();
            string grade = gradeId;

            var Id = InputNameGrade.SelectedItem as Grade;
            if (Id != null)
            {
                gradeId = Id.Id.ToString().ToUpper();
            }

            string Info = InputInfo.Text.Trim();// получаем вводимый текст в поле прайс

            if (NameWorker.Length == 0 || Info.Length == 0 || gradeId.Length == 0) //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Заполните пустые поля!");
            }
            else
            {
                MessageBox.Show("Сотрудник добавлен!");
                Worker worker = new Worker( ID, NameWorker, gradeId, grade, Info);
                db.Workers.Add(worker);//Добавление и сохранение данных в таблицу Worker
                db.SaveChanges();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
