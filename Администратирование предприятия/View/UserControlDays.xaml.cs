using System;
using System.Collections;
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
using Администратирование_предприятия.Models;

namespace Администратирование_предприятия.View
{
    /// <summary>
    /// Логика взаимодействия для UserControlDays.xaml
    /// </summary>
    public partial class UserControlDays : UserControl
    {
        ApplicationContext db = new ApplicationContext();
        public static string? numberDayUC;

        public UserControlDays()
        {
            InitializeComponent();
        }

        private List<TimeBox>? BookedTimeBoxes()
        {
            AddDateTime addDateTime = new AddDateTime();
            var uniqueTimeBoxes = AddDateTime.uniqueTimeBoxesBlocked;
            return uniqueTimeBoxes;
        }

        public void days(int numberDay)
        {
            //AddDateTime addDateTime = new AddDateTime();
            NumberDay.Text = numberDay + "";

            DateTime today = DateTime.Today;
            int dayOfMonth = today.Day;
            int month = today.Month;
            int year = today.Year;
            
            var uniqueTimeBoxes = BookedTimeBoxes();

            if (uniqueTimeBoxes.Count == 0 && db.SelectedDateTimeBoxes.Any(p => p.Date == ViewCalendar.currentYear + "/" + ViewCalendar.currentMonth + "/" + NumberDay.Text + "/")) 
            {
                BackGrid.Background = Brushes.Coral;
                BackGrid.IsEnabled = false;
            }

            if (Convert.ToInt32(NumberDay.Text) < dayOfMonth && ViewCalendar.currentMonth == month && ViewCalendar.currentYear == year)
            {
                BackGrid.Background = Brushes.Coral;
                BackGrid.IsEnabled = false;
            }
        }

        private void AddClickTime(object sender, RoutedEventArgs e)
        {
            numberDayUC = NumberDay.Text;
            AddDateTime addTime = new AddDateTime();
            addTime.Show();
        }
    }
}
