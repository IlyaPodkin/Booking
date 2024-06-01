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
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public Products()
        {
            InitializeComponent();
            Loaded += Products_Loaded;//отработчик события для БД
            DataGridProducts.ItemsSource = db.Products.Local.ToBindingList();//Вывод записей из бд
        }
        ApplicationContext db = new ApplicationContext();//Подключение БД
        void Products_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Products.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Products.Local.ToObservableCollection();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.Show();
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Product product = (Product)DataGridProducts.SelectedItem;
            // получаем измененный объект
            if (product != null)
            {
                db.SaveChanges();
            }
            this.NavigationService.Refresh();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            Product product = (Product)DataGridProducts.SelectedItem;
            if (product == null)
            {
                MessageBox.Show("Выберите запись для удаления!");
            }
            else
            {
                try
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                }
                catch
                {
                    db.Update(product);
                    MessageBox.Show("Невозможно удалить запись, так как эта категория используются в других таблицах!");
                }
            }
        }
    }
}
