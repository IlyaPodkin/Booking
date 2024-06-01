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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Администратирование_предприятия.Models;

namespace Администратирование_предприятия.View
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
            Loaded += Products_Loaded;//отработчик события для БД

        }
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void Products_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Products.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Products.Local.ToObservableCollection();
        }
        private void ButtonEntryRegClick(object sender, RoutedEventArgs e)
        {
            Guid id = Guid.NewGuid();//генерация гуида(ID) в бд
            string name = InputName.Text.Trim();// получаем вводимый текст в поле наименование услуги
            int price = Convert.ToInt32(InputPrice.Text.Trim());// получаем вводимый текст в поле наименование услуги
            string info = InputInfo.Text.Trim();// получаем вводимый текст в поле наименование услуги

            if (name.Length == 0 || price == 0 || info.Length == 0) //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Заполните пустые поля!");
            }
            else
            {
                MessageBox.Show("Категория добавлена!");
                Product products = new Product(id, name, price, info);
                db.Products.Add(products);//Добавление и сохранение данных в таблицу Service
                db.SaveChanges();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
