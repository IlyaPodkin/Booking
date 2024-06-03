using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Metadata;
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
using Администратирование_предприятия.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Администратирование_предприятия.View
{
    /// <summary>
    /// Логика взаимодействия для AddTime.xaml
    /// </summary>
    public partial class AddDateTime : Window
    {
        public static List<TimeBox>? uniqueTimeBoxesBlocked;
        public AddDateTime()
        {
            InitializeComponent();
            InputDate.Text = ViewCalendar.currentYear + "/" + ViewCalendar.currentMonth + "/" + UserControlDays.numberDayUC + "/";
            InputDate.IsReadOnly = true;
            BindItemInComboBoxTimeBoxs();
            uniqueTimeBoxesBlocked = TimeBoxs;
        }

        public List<TimeBox>? TimeBoxs { get; set; }
        public List<TimeBox>? UniqueValues { get; set; }
        ApplicationContext db = new ApplicationContext();

        //Биндим ячейки времени в ComboBox
        private void BindItemInComboBoxTimeBoxs()
        {
            var uniqueTimeBoxes = BookedTimeBoxes();
            TimeBoxs = uniqueTimeBoxes;
            InputTime.ItemsSource = TimeBoxs;
        }

        //Закрытие вкладки
        private void ButtonClickExit(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// фильтр для отбора не забронированных записей
        /// </summary>
        /// <returns></returns>
        private List<TimeBox>? BookedTimeBoxes()
        {
            #region Старый вариант фильтрации
            //var uniqueValues = db.TimeBoxs.Where(p => p.WorkerId == AddBooking.WorkerIdForTimeBox).ToList(); //фильтр коллекции по мастерам
            //var ValuesDate = db.SelectedDateTimeBoxes.Where(p => p.Date.Contains(InputDate.Text) && p.WorkerId == AddBooking.WorkerIdForTimeBox).ToList(); //даты которые есть в вспомогательной таблице
            //var ValuesTime = db.SelectedDateTimeBoxes.Select(p => p.Time).ToList(); //время которое есть в вспомогательной таблице 

            //var uniqueValuesTime = ValuesDate.Where(p => ValuesTime.Contains(p.Time) && p.WorkerId == AddBooking.WorkerIdForTimeBox).ToList(); //фильтр коллекции по дате и времени

            //foreach (var uniqueValueTime in uniqueValuesTime)
            //{
            //    uniqueValues = uniqueValues.Where(p => !p.Value.Contains(uniqueValueTime.Time) && p.WorkerId == AddBooking.WorkerIdForTimeBox).ToList();
            //}

            //return uniqueValues;
            #endregion
            // Получаем все временные интервалы для конкретного рабочего
            var workerId = AddBooking.WorkerIdForTimeBox;
            var allTimeBoxes = db.TimeBoxs.Where(p => p.WorkerId == workerId).ToList();

            // Получаем даты и время из вспомогательной таблицы для конкретного рабочего и выбранной даты
            var selectedDate = InputDate.Text;
            var selectedDateTimeBoxes = db.SelectedDateTimeBoxes
                                          .Where(p => p.Date.Contains(selectedDate) && p.WorkerId == workerId)
                                          .Select(p => p.Time) // Берем только время
                                          .ToList();

            // Исключаем занятые временные интервалы одним разом
            allTimeBoxes = allTimeBoxes
                .Where(p => !selectedDateTimeBoxes.Contains(p.Value))
                .ToList();

            // Если нет доступных временных интервалов, возвращаем null
            return allTimeBoxes.Any() ? allTimeBoxes : null;
        } 

        /// <summary>
        /// Кнопка добавления даты и времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClickAddTime(object sender, RoutedEventArgs e)
        {
            Guid id = Guid.NewGuid();
            string date = ViewCalendar.currentYear + "/" + ViewCalendar.currentMonth + "/" + UserControlDays.numberDayUC + "/";
            string time = InputTime.Text;
            string timeBoxesId = "";
            string TimeBoxesId = "";
            AddBooking.Date = date;
            AddBooking.Time = time;
            MessageBox.Show("Дата и время подтверждены. Теперь подтвердите бронирование.");
            var workerId = InputTime.SelectedItem as TimeBox;//достаем ID
            var worker = InputTime.SelectedItem as TimeBox;//достаем ID
            if (workerId != null && worker != null)
            {
                TimeBoxesId = workerId.WorkerId.ToString().ToUpper();
                AddBooking.SelectedTimeBoxId = TimeBoxesId;
                timeBoxesId = worker.Id.ToString().ToUpper();
            }
            SelectedDateTimeBox selectedDateTimeBox = new SelectedDateTimeBox(id, time, date, TimeBoxesId, timeBoxesId);
            db.SelectedDateTimeBoxes.Add(selectedDateTimeBox);
            db.SaveChanges();
            var windowToClose = Application.Current.Windows
                                     .OfType<ViewCalendar>()
                                     .FirstOrDefault();
            // Если такое окно найдено, закройте его
            if (windowToClose != null)
            {
                windowToClose.Close();
            }
            Hide();
        }
    }
}
