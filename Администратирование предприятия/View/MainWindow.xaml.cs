using MaterialDesignThemes.Wpf;
using System.Text;
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
using System.Security.Cryptography;

namespace Администратирование_предприятия
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();//Подключение БД
        //public User? Users { get; private set; }
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // загружаем данные из БД
            db.Users.Load();
            // и устанавливаем данные в качестве контекста
            DataContext = db.Users.Local.ToObservableCollection();
        }
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;//отработчик события для БД
        }
        private void ButtonEntryRegClick(object sender, RoutedEventArgs e)
        {
            Guid ID = Guid.NewGuid();//генерация гуида(ID) в бд
            string login = InputLogin.Text.Trim();// получаем вводимый текст в поле логин
            string password = InputPassword.Password.Trim();// получаем вводимый текст в поле пароль
            string passwordTwo = InputPassTwo.Password.Trim();// получаем вводимый текст в поле повторно вводимый пароль

            string encryptedPassword = password;

            // создаем соль
            byte[] salt = GenerateSalt();

            // создаем MD5-хеш
            byte[] md5Hash = GenerateMD5Hash(encryptedPassword, salt);
            string md5HashString = Convert.ToBase64String(md5Hash);

            string name = InputName.Text.Trim();

            if (!login.Contains("@") || !login.Contains(".")) // проверки на ввод полей регистрации
            {
                InputLogin.ToolTip = "Это поле введено некорректно";
                InputLogin.BorderBrush = Brushes.DarkRed;
            }
            else
            {
                InputLogin.ToolTip = "Успешно";
                InputLogin.BorderBrush = Brushes.Transparent;
            }
            if (name == "") // проверки на ввод полей регистрации
            {
                InputName.ToolTip = "Это поле введено некорректно";
                InputName.BorderBrush = Brushes.DarkRed;
            }
            else
            {
                InputName.ToolTip = "Успешно";
                InputName.BorderBrush = Brushes.Transparent;
            }

            if (password.Length < 7)
            {
                InputPassword.ToolTip = "Это поле введено некорректно";
                InputPassword.BorderBrush = Brushes.DarkRed;
            }
            else
            {
                InputPassword.ToolTip = "Успешно";
                InputPassword.BorderBrush = Brushes.Transparent;
            }

            if (passwordTwo.Length < 7 || passwordTwo != password)
            {
                InputPassTwo.ToolTip = "Это поле введено некорректно";
                InputPassTwo.BorderBrush = Brushes.DarkRed;
            }
            else
            {
                InputPassTwo.ToolTip = "Успешно";
                InputPassTwo.BorderBrush = Brushes.Transparent;
            }

            if (!login.Contains("@") || !login.Contains(".") || password.Length < 7 || passwordTwo.Length < 7 || passwordTwo != password || name == "") //вывод результата ввода и добавление значений в бд
            {
                MessageBox.Show("Некорретные данные!");
            }
            else
            {
                User? User = null;

                if (User != db.Users.Where(user => user.Login == login || user.Password == password).FirstOrDefault()) 
                {
                    MessageBox.Show("Такой пользователь уже зарегистрирован в системе или существет такой же логин или пароль. Введите другие данные");
                }
                else 
                {
                    MessageBox.Show("Регистрация прошла успешно!");
                    User user = new User(ID, login, md5HashString, name);
                    db.Users.Add(user);//Добавление и сохранение данных в таблицу USERS
                    db.SaveChanges();
                }
                
            }
        }
        static byte[] GenerateMD5Hash(string encryptedPassword, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(encryptedPassword);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

            var hash = new MD5CryptoServiceProvider();

            return hash.ComputeHash(saltedPassword);
        }
        static byte[] GenerateSalt()
        {
            const int SaltLength = 64;
            byte[] salt = new byte[SaltLength];

            var rngRand = new RNGCryptoServiceProvider();
            rngRand.GetBytes(salt);

            return salt;
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            Hide();
        }
    }
}