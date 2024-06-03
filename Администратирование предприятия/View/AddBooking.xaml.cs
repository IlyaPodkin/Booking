using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;
using Администратирование_предприятия.View;

namespace Администратирование_предприятия
{
    public partial class AddBooking : Window
    {
        public static string? Date { get; set; }
        public static string? Time { get; set; }
        public static string? WorkerIdForTimeBox;
        public static string? SelectedTimeBoxId;

        ApplicationContext db = new ApplicationContext();//Подключение БД

        void Booking_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Bookings.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Bookings.Local.ToObservableCollection();
        }

        public AddBooking()
        {
            InitializeComponent();
            Loaded += Booking_Loaded;//отработчик события для БД
            BindItemInComboBoxWorker();
            BindItemInComboBoxService();
            BindItemInComboBoxProduct();
            BindItemInComboBoxTimeBox();
            InputDate.IsReadOnly = true;
            InputTime.IsReadOnly = true;
        }

        public List<Worker>? workers { get; set; }
        public List<Service>? services { get; set; }
        public List<Product>? products { get; set; }
        public List<TimeBox>? TimeBoxes { get; set; }

        private void BindItemInComboBoxWorker()
        {
            var worker = db.Workers.ToList();
            workers = worker;
            InputNameWorker.ItemsSource = workers;
        }
        private void BindItemInComboBoxService()
        {
            var service = db.Services.ToList();
            services = service;
            InputNameService.ItemsSource = services;
        }
        private void BindItemInComboBoxProduct()
        {
            var product = db.Products.ToList();
            products = product;
            InputNameProduct.ItemsSource = products;
        }
        private void BindItemInComboBoxTimeBox()
        {
            var timeBox = db.TimeBoxs.ToList();
            TimeBoxes = timeBox;
            InputTime.ItemsSource = TimeBoxes;
        }

        private void ButtonEntryRegClick(object sender, RoutedEventArgs e)
        {
            AddBookingDb();
        }
        /// <summary>
        /// Событие на фильтрацию ComboBox Мастеров и услуг
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputNameWorker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = InputNameWorker.SelectedItem as Worker;
            if (selectedCategory == null)
            {
                InputNameService.ItemsSource = db.Services.ToList();
            }
            else
            {
                InputNameService.ItemsSource = db.Services.Where(p=>p.GradeId == selectedCategory.GradeId).ToList();
            }
        }

        private void AddBookingDb() //добавление бронирования в БД
        {
            Guid ID = Guid.NewGuid();//генерация гуида(ID) в бд

            string NameClient = InputNameClient.Text.Trim();
            string NumberPhone = InputNumberPhone.Text.Trim();

            string NameWorkerId = InputNameWorker.Text.Trim();// получаем вводимый текст в поле наименование услуги
            string NameWorker = NameWorkerId;

            string NameServiceId = InputNameService.Text.Trim();
            string NameService = NameServiceId;

            string? NameProductId = null;
            NameProductId = InputNameProduct.Text.Trim();// получаем вводимый текст в поле наименование услуги
            string NameProduct = NameProductId;

            int Price = Convert.ToInt32(InputPrice.Text.Trim());

            string date = InputDate.Text.Trim();

            string selectedDateTimeBoxesId = "";

            string time = InputTime.Text.Trim();

#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            string Info = InputInfo.Text.Trim();
            string isChecked = "Нет";

            var IdService = InputNameService.SelectedItem as Service;//достаем ID
            if (IdService != null)
            {
                NameServiceId = IdService.Id.ToString().ToUpper();
            }

            var IdWorker = InputNameWorker.SelectedItem as Worker;//достаем ID
            if (IdWorker != null)
            {
                NameWorkerId = IdWorker.Id.ToString().ToUpper();
            }

            var IdProduct = InputNameProduct.SelectedItem as Product;//достаем ID
            if (IdProduct != null)
            {
                NameProductId = IdProduct.Id.ToString().ToUpper();
            }
            else 
            {
                NameProductId = null;
            }

            var IdDateTime = db.SelectedDateTimeBoxes.Where(p => p.Time == time || p.Date == date).Select(p => p.Id.ToString());//достаем ID
            foreach (var id in IdDateTime)
            {
                selectedDateTimeBoxesId = id.ToString();
            }
            //selectedDateTimeBoxesId = db.SelectedDateTimeBoxes.Where(p => p.Time == time || p.Date == date).Select(p => p.Id.ToString());


            if (NameClient.Length == 0 || NumberPhone.Length == 0 || NameWorker.Length == 0 || NameService.Length == 0 || date.Length == 0 || time.Length == 0 || Price == 0) //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Заполните пустые обязательные поля!");
            }
            else
            {
                Booking booking = new Booking(ID, NameClient, NumberPhone, NameWorker, NameService, NameProduct, NameWorkerId, NameServiceId, NameProductId, selectedDateTimeBoxesId, Price, date, time, Info, isChecked);
                db.Bookings.Add(booking);//Добавление и сохранение данных в таблицу Worker
                db.SaveChanges();
                MessageBox.Show("Бронирование добавлено!");
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void ResultSumPrice(object sender, RoutedEventArgs e)// автоматический выбор цены, за счет выбора индекса записи
        {
            if (InputNameService.SelectedItem != null || InputNameProduct.SelectedItem != null)
            {
                int sum;
                var priceService = InputNameService.SelectedItem as Service;//достаем цену за услугу
                var priceProduct = InputNameProduct.SelectedItem as Product;//достаем цену за товар

                if (priceService == null || priceProduct == null)
                {
                    if (priceService != null)
                    {
                        sum = Convert.ToInt32(priceService.Price);
                        InputPrice.Text = sum.ToString();
                    }
                    else
                    {
                        sum = Convert.ToInt32(priceProduct.Price);
                        InputPrice.Text = sum.ToString();
                    }
                }
                else
                {
                    sum = Convert.ToInt32(priceService.Price) + Convert.ToInt32(priceProduct.Price);
                    InputPrice.Text = sum.ToString();
                }
                InputDate.Text = Date;
                InputTime.Text = Time;
            }
        }

        private void ButtonClickSelectedDate(object sender, RoutedEventArgs e)
        {
            var IdWorker = InputNameWorker.SelectedItem as Worker;//достаем ID
            if (IdWorker != null)
            {
                WorkerIdForTimeBox = IdWorker.Id.ToString().ToUpper();
            }
            ViewCalendar calendar = new ViewCalendar();
            calendar.Show();
        }

        
    }
}
