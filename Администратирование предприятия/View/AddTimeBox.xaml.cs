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
    /// Логика взаимодействия для AddTimeBox.xaml
    /// </summary>
    public partial class AddTimeBox : Window
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД
        public AddTimeBox()
        {
            InitializeComponent();
            Loaded += TimeBox_Loaded;//отработчик события для БД
            BindItemInComboBoxWorker();
        }
        void TimeBox_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.TimeBoxs.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.TimeBoxs.Local.ToObservableCollection();
        }
        //Биндим имена сотрудников в ComboBox
        public List<Worker>? workers { get; set; }
        private void BindItemInComboBoxWorker()
        {
            var worker = db.Workers.ToList();
            workers = worker;
            InputNameWorker.ItemsSource = workers;
        }
        //Закрытие окна
        private void ButtonExitClick(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ButtonAddTimeBoxClick(object sender, RoutedEventArgs e)
        {
            AddTimeBoxs();
        }
        private void AddTimeBoxs()
        {
            Guid Id = Guid.NewGuid();//генерация гуида(ID) в бд
            string TimeBoxValue = InputTimeBox.Text.Trim();// получаем вводимый текст в поле ввода времени
            string NameWorkerId = InputNameWorker.Text.Trim();// получаем вводимый текст в поле выбора мастера
            string NameWorker = NameWorkerId;

            var IdWorker = InputNameWorker.SelectedItem as Worker;//достаем ID мастера
            if (IdWorker != null)
            {
                NameWorkerId = IdWorker.Id.ToString().ToUpper();
            }

            if (TimeBoxValue.Length == 0) //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Заполните пустые поля!");
            }
            else
            {
                MessageBox.Show("Доступное время добавлено!");
                TimeBox timeBoxValue = new TimeBox(Id, TimeBoxValue, NameWorkerId, NameWorker);
                db.TimeBoxs.Add(timeBoxValue);//Добавление и сохранение данных в таблицу TimeBoxs
                db.SaveChanges();
            }
        }
    }
}
