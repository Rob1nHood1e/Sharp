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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Req.xaml
    /// </summary>
    /// 
    public interface Showtime
    {
         void Look(string str);
         int Num();
    }

    class Sh : Showtime
    {
        int Showtime.Num()
        {
            Random rnd = new Random();
            return (rnd.Next(3, 6));
        }

        void Showtime.Look(string str)
        {
            string s = "А можно " + str + "?";
            MessageBox.Show(s);
        }
    }

    public partial class Req : Window
    {

        public Req()
        {
            InitializeComponent();
        }


        public void N(Showtime r)
        {
            int f = r.Num();
            r.Look(f.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sh s = new Sh();
            N(s);
        }
    }
}
