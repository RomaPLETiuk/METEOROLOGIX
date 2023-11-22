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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BD_1_1_n
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Створіть анімацію прогресу завантаження
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 100,
                Duration = TimeSpan.FromSeconds(5), // Тривалість анімації (5 секунд)
                FillBehavior = FillBehavior.Stop
            };

            // Підключіть обробник події завершення анімації
            animation.Completed += Animation_Completed;

            // Запустіть анімацію
            loadingProgressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }

        private void Animation_Completed(object sender, EventArgs e)
        {
            // Після завершення анімації можна виконати додаткові дії, наприклад, закрити вікно завантаження.
            Auht auht = new Auht();
            auht.Show();
            Close();

        }
    }
}
