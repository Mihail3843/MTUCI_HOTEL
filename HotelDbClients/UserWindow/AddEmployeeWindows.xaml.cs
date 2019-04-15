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
    /// Логика взаимодействия для AddEmployeeWindows.xaml
    /// </summary>
    public partial class AddEmployeeWindows : Window
    {
        public AddEmployeeWindows()
        {
            InitializeComponent();
        }

        private async void AddClients(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into Employees values(@Surname," +
                " @Name, @Patronymic,@Position, @DateBirth, @DateEmployee, @DateDismiss," +
                " @Education, @National, @Passport)", ConnectionDB.ConnectionSt);
            command.Parameters.AddWithValue("@Surname", SurnameTbx.Text);
            command.Parameters.AddWithValue("@Name", NameTbx.Text);
            command.Parameters.AddWithValue("@Patronymic", PatronymicTbx.Text);
            command.Parameters.AddWithValue("@Passport", PassportTbx.Text);
            command.Parameters.AddWithValue("@DateBirth", BirthDateTbx.Text);
            command.Parameters.AddWithValue("@DateEmployee", DateEmployeeTbx.Text);
            command.Parameters.AddWithValue("@DateDismiss", DateDismissTbx.Text);
            command.Parameters.AddWithValue("@Position", PositionTbx.Text);
            command.Parameters.AddWithValue("@Education", EducationTbx.Text);
            command.Parameters.AddWithValue("@National", NationalyTbx.Text);
            //try
            //{
                await command.ExecuteNonQueryAsync();
                //this.Close();
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Во время выполнения произошла ошибка. Проверьте соединение с интернетом или обратитесь к администратору");
            //}
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
