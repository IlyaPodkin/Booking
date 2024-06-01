using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using Администратирование_предприятия.View;

namespace Администратирование_предприятия
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void ConfirmationOfRegistration_Click(object sender, RoutedEventArgs e)
        {
            ApplicationContext db = new ApplicationContext();

            string login = InputLogin.Text.Trim();// получаем вводимый текст в поле логин
            string password = InputPassword.Password.Trim();// получаем вводимый текст в поле пароль

            // создаем соль
            byte[] salt = GenerateSalt();

            // создаем MD5-хеш
            byte[] md5Hash = GenerateMD5Hash(password, salt);
            string md5HashString = Convert.ToBase64String(md5Hash);

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

            User? User = null;
            var existUser = db.Users.Select(u => u);
            User = db.Users.Where(user => user.Login == login && user.Password == md5HashString).FirstOrDefault();
            if (existUser.Count() != 0)
            {
                if (!login.Contains("@") || !login.Contains(".") || password.Length < 7) //вывод результата ввода и добавление значений в бд
                {
                    MessageBox.Show("Некорретные данные!");
                }
                else
                {
                    if (User == null)
                    {
                        MessageBox.Show("Пользователь не найден!");
                    }
                    else
                    {
                        MessageBox.Show("Вход в учетную запись выполнен!");
                        HomePage homePage = new HomePage();
                        homePage.Show();
                        Hide();
                    }
                }
            }
            else
            {
                if (InputLogin.Text == "@." && InputPassword.Password == "1111111")
                {
                    MessageBox.Show("Вход в учетную запись выполнен!");
                    HomePage homePage = new HomePage();
                    homePage.Show();
                    Hide();
                }
                else 
                {
                    MessageBox.Show("Некорретные данные!");
                }
            }
        }

        static byte[] GenerateMD5Hash(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
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
            ButtonShowRestration();
        }

        private void ButtonShowUsers_Click(object sender, RoutedEventArgs e)
        {
            ButtonShowUser();
        }

        private void ButtonShowUser()
        {
            string password = InputPassword.Password.Trim();// получаем вводимый текст в поле пароль

            // создаем соль
            byte[] salt = GenerateSalt();

            // создаем MD5-хеш
            byte[] md5Hash = GenerateMD5Hash(password, salt);
            string md5HashString = Convert.ToBase64String(md5Hash);

            ApplicationContext db = new ApplicationContext();
            var existUser = db.Users.Select(u => u);
            var user = db.Users.Where(user => user.Login == InputLogin.Text && user.Password == md5HashString).FirstOrDefault();

            if (existUser.Count() != 0)
            {
                if (!InputLogin.Text.Contains("@") || !InputLogin.Text.Contains(".") || password.Length < 7) //вывод результата ввода и добавление значений в бд
                {
                    MessageBox.Show("Некорретные данные!");
                }
                else
                {
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден!");
                    }
                    else
                    {
                        ShowUsers showUsers = new ShowUsers();
                        showUsers.Show();
                        Hide();
                    }
                }
            }
            else
            {
                if (InputLogin.Text == "@." && InputPassword.Password == "1111111")
                {
                    ShowUsers showUsers = new ShowUsers();
                    showUsers.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Некорретные данные!");
                }
            
            }
        }
        private void ButtonShowRestration()
        {
            string password = InputPassword.Password.Trim();// получаем вводимый текст в поле пароль

            // создаем соль
            byte[] salt = GenerateSalt();

            // создаем MD5-хеш
            byte[] md5Hash = GenerateMD5Hash(password, salt);
            string md5HashString = Convert.ToBase64String(md5Hash);

            ApplicationContext db = new ApplicationContext();
            var existUser = db.Users.Select(u => u);
            var user = db.Users.Where(user => user.Login == InputLogin.Text && user.Password == md5HashString).FirstOrDefault();

            if (existUser.Count() != 0)
            {
                if (!InputLogin.Text.Contains("@") || !InputLogin.Text.Contains(".") || password.Length < 7) //вывод результата ввода и добавление значений в бд
                {
                    MessageBox.Show("Некорретные данные!");
                }
                else
                {
                    if (user == null)
                    {
                        MessageBox.Show("Пользователь не найден!");
                    }
                    else
                    {
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        Hide();
                    }
                }
            }
            else
            {
                if (InputLogin.Text == "@." && InputPassword.Password == "1111111")
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Некорретные данные!");
                }
            
            }
        }
    }
}
