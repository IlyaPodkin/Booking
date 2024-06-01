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
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД
        //public Service? Servise { get; private set; }
        void Service_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Services.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Services.Local.ToObservableCollection();
        }

        public AddService()
        {
            InitializeComponent();
            Loaded += Service_Loaded;//отработчик события для БД
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
            string NameService = InputNameServices.Text.Trim();// получаем вводимый текст в поле наименование услуги
            int Price = Convert.ToInt32(InputPrice.Text.Trim());// получаем вводимый текст в поле прайс
            string Time = InputTime.Text.Trim();// получаем вводимый текст в поле время

            string gradeId = InputNameGrade.Text.Trim();
            string grade = gradeId;

            var Id = InputNameGrade.SelectedItem as Grade;
            if (Id != null)
            {
                gradeId = Id.Id.ToString().ToUpper();
            }

            if (NameService.Length == 0 || Price == 0 || Time.Length == 0 || gradeId.Length == 0) //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Заполните пустые поля!");
            }
            else
            {
                MessageBox.Show("Услуга добавлена!");
                Service Service = new Service(ID, NameService, grade, gradeId, Price, Time);
                db.Services.Add(Service);//Добавление и сохранение данных в таблицу Service
                db.SaveChanges();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
