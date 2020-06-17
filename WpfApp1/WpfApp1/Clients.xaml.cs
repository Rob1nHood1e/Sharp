using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WpfApp1
{
    public partial class Clients : Window
    {
        string connectionString;
        SqlDataAdapter adapter;
        DataTable clientsTable;

        public Clients()
        {
            InitializeComponent();
            // получаем строку подключения из app.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            clientsGrid.RowEditEnding += ClientsGrid_RowEditEnding;
        }

        private void ClientsGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            UpdateDB();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM clientsTable";
            clientsTable = new DataTable();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                // установка команды на добавление для вызова хранимой процедуры
                adapter.InsertCommand = new SqlCommand("sp_InsertClients", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@FullName", SqlDbType.NVarChar, 50, "FullName"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@PassID", SqlDbType.NVarChar, 50, "PassID"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Debt", SqlDbType.Int, 0, "Debt"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@Id", SqlDbType.Int, 0, "Id");
                parameter.Direction = ParameterDirection.Output;

                connection.Open();
                adapter.Fill(clientsTable);
                clientsGrid.ItemsSource = clientsTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void UpdateDB()
        {
            SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
            try
            {
                adapter.Update(clientsTable);
            }
            catch (SqlException)
            {
                MessageBox.Show("Пожалуйста, заполните пустые поля");
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDB();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (clientsGrid.SelectedItems != null)
            {
                for (int i = 0; i < clientsGrid.SelectedItems.Count; i++)
                {
                    DataRowView datarowView = clientsGrid.SelectedItems[i] as DataRowView;
                    if (datarowView != null)
                    {
                        DataRow dataRow = (DataRow)datarowView.Row;
                        dataRow.Delete();
                    }
                }
            }
            UpdateDB();
        }
    }
}