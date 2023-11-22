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
using System.Windows.Shapes;

namespace BD_1_1_n
{
    /// <summary>
    /// Логика взаимодействия для Auht.xaml
    /// </summary>
    public partial class Auht : Window
    {
        private DatabaseHelper dbHelper;

        public Auht()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = passwordBox1.Password;

            try
            {
                dbHelper = new DatabaseHelper("localhost", 5432, username, password, "MyDB1");
                dbHelper.OpenConnection(); // Власний метод для відкриття підключення
                                           // Відкриваємо нове вікно для роботи з базою даних
                var databaseWindow = new DatabaseWindow(dbHelper, username);
                databaseWindow.Show();
                Close(); // Закриваємо поточне вікно
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка підключення до бази даних: {ex.Message}");
            }
        }
    }
}
