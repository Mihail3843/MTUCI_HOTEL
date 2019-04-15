using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace HotelDbClients.UserWindow
{
    /// <summary>
    /// Логика взаимодействия для AddClientsWindow.xaml
    /// </summary>
    public partial class AddClientsWindow : Window
    {
        public AddClientsWindow()
        {
            InitializeComponent();
        }

        private async void AddClients(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Clients values(@Surname, @Name, @Patronymic, @Passport, @DateSettlement, @DateEviction, @NomberRoom, '3')", ConnectionDB.ConnectionSt);
            command.Parameters.AddWithValue("@Surname", SurnameTbx.Text);
            command.Parameters.AddWithValue("@Name", NameTbx.Text);
            command.Parameters.AddWithValue("@Patronymic", PatronymicTbx.Text);
            command.Parameters.AddWithValue("@Passport", PassportTbx.Text);
            command.Parameters.AddWithValue("@DateSettlement", firstDP.SelectedDate );
            command.Parameters.AddWithValue("@DateEviction", secondDP.SelectedDate);
            command.Parameters.AddWithValue("@NomberRoom", RoomNumbTbx.Text);
            try
            {                
               await command.ExecuteNonQueryAsync();
               MessageBox.Show("Запись Добавлена");
               this.Close();
        }
            catch (Exception)
            {
                MessageBox.Show("Во время выполнения произошла ошибка. Проверьте соединение с интернетом или обратитесь к администратору");              
            }
}

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
