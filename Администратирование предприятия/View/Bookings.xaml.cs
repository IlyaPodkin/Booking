using System.Data.Common;
using System.Data.Entity;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Администратирование_предприятия
{
    public partial class Bookings : Page
    {
        public Bookings()
        {
            InitializeComponent();
            Loaded += Booking_Loaded;//отработчик события для БД
            //Loaded += Profit_Loaded;//отработчик события для БД
            DataGridBooking.ItemsSource = db.Bookings.Local.ToBindingList();//Вывод записей из бд
            SumProfit();
        }
        #region
        //void Profit_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // загружаем данные из БД
        //    db.Profits.Load();
        //    // и устанавливаем данные в качестве контекста
        //    DataContext = db.Profits.Local.ToObservableCollection();
        //}
        #endregion
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void Booking_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Bookings.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Bookings.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBooking addBooking = new AddBooking();
            addBooking.Show();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Booking booking = (Booking)DataGridBooking.SelectedItem;
            // получаем измененный объект
            if (booking != null)
            {
                db.SaveChanges();
            }
            this.NavigationService.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {   
            Booking booking = (Booking)DataGridBooking.SelectedItem;
            var selectedBox = booking.SelectedDateTimeBoxesId;
            var selectedDateTimeBoxes = db.SelectedDateTimeBoxes.Where(p=>p.Id.ToString() == selectedBox.ToString());
            SelectedDateTimeBox? selectedDateTimeBox = selectedDateTimeBoxes.FirstOrDefault();
            if (booking == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                db.Bookings.Remove(booking);
                if (selectedDateTimeBox != null) 
                {
                    db.SelectedDateTimeBoxes.Remove(selectedDateTimeBox);
                }
                db.SaveChanges();
                SumProfit();
            }
        }

        //Зачисление выручки на баланс
        private void BalanceButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Booking booking = (Booking)DataGridBooking.SelectedItem;
            // получаем измененный объект
            if (booking == null)
            {
                MessageBox.Show("Нет записей бронирования");
            }
            else
            {
                if (booking.IsChecked == "Нет")
                {
                    booking.IsChecked = "Да";
                    db.SaveChanges();
                    // если вдруг надумаю делать отдельную таблицу для баланса
                    #region
                    //зачисляем выручку на баланс и добавляем запись в таблицу Balance

                    //Guid Id = Guid.NewGuid();//генерация гуида(ID) в бд
                    //string price = "";
                    //string priceId = "";

                    //var IdPrice = DataGridBooking.SelectedItem as Booking;//достаем ID
                    //if (IdPrice != null)
                    //{
                    //    priceId = IdPrice.Id.ToString().ToUpper();
                    //}
                    //var Price = DataGridBooking.SelectedItem as Booking;//достаем ID
                    //if (Price != null)
                    //{
                    //    price = Price.Price.ToString();
                    //}

                    //Profit profit = new Profit(Id, price, priceId);
                    //db.Profits.Add(profit);//Добавление и сохранение данных в таблицу Worker
                    //db.SaveChanges();

                    //SumProfit();
                    #endregion
                    SumProfit();
                    MessageBox.Show("Выручка зачислена на баланс.");
                }
                else
                {
                    MessageBox.Show("Внимание! Эта выручка уже зачислена на баланс!");
                }
                CollectionViewSource.GetDefaultView(DataGridBooking.ItemsSource).Refresh(); // Кто придумал эту вещь тот боженька, обновляет датагриб
            }
        }
        private void SumProfit()
        {
            var booking = db.Bookings.Where(p => p.IsChecked.Contains("Да"));

            if (booking.Any(p => p.IsChecked == "Да"))
            {
                var PriceSum = booking.Sum(p => p.Price);
                InputBalance.Text = PriceSum.ToString();
            }
            else 
            {
                InputBalance.Text = "0";
            }
        }
    }
}
