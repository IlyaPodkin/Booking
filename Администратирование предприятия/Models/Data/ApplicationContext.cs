using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Администратирование_предприятия.Models
{
    class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;//Добавление данных в таблицу Users
        public DbSet<Product> Products { get; set; } = null!;//Добавление данных в таблицу Products
        public DbSet<Grade> Grades { get; set; } = null!;//Добавление данных в таблицу Grades
        public DbSet<Service> Services { get; set; } = null!;//Добавление данных в таблицу Services
        public DbSet<Worker> Workers { get; set; } = null!;//Добавление данных в таблицу Workers
        public DbSet<TimeBox> TimeBoxs { get; set; } = null!;//Добавление данных в таблицу TimeBoxs
        public DbSet<SelectedDateTimeBox> SelectedDateTimeBoxes { get; set; } = null!;//Добавление данных в таблицу SelectedDateTimeBoxes
        public DbSet<Booking> Bookings { get; set; } = null!;//Добавление данных в таблицу Бронирования записей (Bookings)
        
        public ApplicationContext() // если БД нет, то этот метод создаст её
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)//Строка подключения БД
        {
            optionsBuilder.UseSqlite("Data Source=BarBer.db");
        }
    }
}
