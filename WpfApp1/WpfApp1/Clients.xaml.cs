using System.Windows;
using System.Data.Entity;
using WpfApp1.Requests;
using System.Windows.Controls;
using System.Windows.Data;
using System;
using System.Data.SqlClient;

namespace WpfApp1
{
    delegate void Saving();
   
    public partial class Clients : Window
    {
        Saving s;
        Context db;
        public Clients()
        {
            InitializeComponent();
            s = Save;
            db = new Context();
            db.clientsTable.Load(); // загружаем данные
            clientsGrid.ItemsSource = db.clientsTable.Local.ToBindingList(); // устанавливаем привязку к кэшу

            this.Closing += MainWindow_Closing;
        }


        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                MessageBox.Show("Пожалуйста, заполните данные корректно");
            }
        }



        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            s();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientsGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < clientsGrid.SelectedItems.Count; i++)
                {
                    Client client = clientsGrid.SelectedItems[i] as Client;
                    if (client != null)
                    {
                        db.clientsTable.Remove(client);
                    }
                }
            }
            s();
        }
    }
}