using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> styles = new List<string> { "green", "blue" };
        }

        private MediaPlayer player = new MediaPlayer();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            player.Open(new Uri("music.MP3", UriKind.Relative));
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clients clients = new Clients();
            clients.Show();

        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {

            string msg = "Вы уверены что хотите выйти?";
            MessageBoxResult result =
              MessageBox.Show(
                msg,
                "Выход",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                // If user doesn't want to close, cancel closure
                e.Cancel = true;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void Styles(string color)
        {
            var uri = new Uri(color + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }      
        
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Styles("Green");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Styles("Blue");
        }



        private void HotKey(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "F1")  Styles("Green");
            if (e.Key.ToString() == "F2") Styles("Blue");
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            player.Play();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            player.Pause();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            popup1.IsOpen = true;
        }
    }
}
