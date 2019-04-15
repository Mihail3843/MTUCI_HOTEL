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
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelDbClients.UserWindow
{
    /// <summary>
    /// Логика взаимодействия для AddRequest.xaml
    /// </summary>
    public partial class AddRequest : Window
    {
        List<string> Clients;
        List<string> Services;

        public AddRequest()
        {
            Clients = new List<string>();
            Services = new List<string>();
            InitializeComponent();
            GetClient();
        }

        async void GetClient()
        {
            if (ConnectionDB.ConnectionSt != null && ConnectionDB.ConnectionSt.State == System.Data.ConnectionState.Open)
            {
                SqlDataReader datareader = null;
                SqlDataReader datareader2 = null;
                SqlCommand commad = new SqlCommand("select cast(ClientID as nvarchar(10)) + ' '+ SurName + '; ' + Name  + ' ' + Patronymic as ResultCol " +
                    " from Clients", ConnectionDB.ConnectionSt);
                SqlCommand comman2 = new SqlCommand("select cast(ServicesID as nvarchar(10)) +' '+  ServicesName + ' ' + cast(ServicesPrice as nvarchar(10))" +
                    "+'руб' as result_man from HotelServices", ConnectionDB.ConnectionSt);
                //try
                //{
                    datareader = await commad.ExecuteReaderAsync();                    
                    while (await datareader.ReadAsync())                    
                        Clients.Add(Convert.ToString(datareader["ResultCol"]));                                            
                datareader.Close();
                datareader2 = await comman2.ExecuteReaderAsync();
                while (await datareader2.ReadAsync())                    
                        Services.Add(Convert.ToString(datareader2["result_man"]));                                            
                datareader2.Close();
                NameCbx.ItemsSource = Clients;
                ServicesTbx.ItemsSource = Services;
                //}
                //catch (Exception ex)
                //{

                //}
                //finally
                //{
                //    datareader.Close();
                //}
            }            
        }
        

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddRequestInsert(object sender, RoutedEventArgs e)
        {            
            if (NameCbx.SelectedIndex == -1 | ServicesTbx.SelectedIndex == -1 )
            {
                MessageBox.Show("Выбраны не все параметры заявки");
                return;
            }
            if (ConnectionDB.ConnectionSt !=null && ConnectionDB.ConnectionSt.State == System.Data.ConnectionState.Open)
            {
                SqlCommand command = new SqlCommand("insert into ServicesRequest values(@ServicesID, @RequestState, @ClientID, @DesacRequest)", ConnectionDB.ConnectionSt);
                command.Parameters.AddWithValue("@ServicesID", Convert.ToInt16(Regex.Match((ServicesTbx.SelectedItem as string), @"\b[0-9]\b").Value));
                command.Parameters.AddWithValue("@RequestState", "принята");
                command.Parameters.AddWithValue("@ClientID", Convert.ToInt16(Regex.Match((NameCbx.SelectedItem as string), @"\b[0-9]\b").Value));
                command.Parameters.AddWithValue("@DesacRequest", FieldDescRequest.Text == "" ? "без коментария" : FieldDescRequest.Text);
                //try
                //{
                    await command.ExecuteNonQueryAsync();
                    MessageBox.Show("Заявка приянта и обрабатывается");
                    this.Close();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Во время выполнения запроса произошла ошибка. Проверьте подключение к сети интернет или обратитесь к администратору");
                //}
            }


        }
    }  
}
