using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
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

namespace Администратирование_предприятия.View
{
    /// <summary>
    /// Логика взаимодействия для Calendar.xaml
    /// </summary>
    public partial class ViewCalendar : Window
    {
        public static DateTime currentDT = DateTime.Now;
        public static int currentYear = currentDT.Year;
        public static int currentMonth = currentDT.Month;

        public ViewCalendar()
        {
            InitializeComponent();
            DisplayCalendar();
        }

        private void DisplayCalendar()
        {
            string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(currentMonth);
            NameMouth.Text = monthName.ToUpper() + " " + currentYear;

            DateTime startofthemonth = new DateTime(currentYear, currentMonth, 1);

            int days = DateTime.DaysInMonth(currentYear, currentMonth) + 1;
            int dayoftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) - 1;
            for (int i = -7; i < dayoftheweek; i++)
            {
                UserControlBlank ucblank = new UserControlBlank();
                DayContainer.Children.Add(ucblank);
            }
            for (int i = 1; i < days; i++)
            {
                UserControlDays ucdays = new UserControlDays();
                ucdays.days(i);
                DayContainer.Children.Add(ucdays);
            }
        }
        //Закрытие окна выбора даты
        private void ButtonExitClick(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        //Пролистать до следущего месяца
        private void ButtonClickNext(object sender, RoutedEventArgs e)
        {
            DayContainer.Children.Clear();
            if (currentMonth == 12)
            {
                currentMonth = 0;
                currentYear++;
            }
            currentMonth++;
            DisplayCalendar();
        }

        //Пролистать до предыдущего месяца
        private void ButtonClickBack(object sender, RoutedEventArgs e)
        {
            DateTime today = DateTime.Today;
            int month = today.Month;
            int year = today.Year;

            if (currentMonth <= month && currentYear <= year)
            {
                MessageBox.Show("Бронирование за прошедший месяц недоступно");
            }
            else
            {
                DayContainer.Children.Clear();
                if (currentMonth == 1)
                {
                    currentMonth = 13;
                    currentYear--;
                }
                currentMonth--;
                DisplayCalendar();
            }
        }
    }
}

