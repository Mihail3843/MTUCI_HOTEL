using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
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
using HotelDbClients.UserWindow;
using System.Windows.Shapes;
using System.Data;

namespace HotelDbClients
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SqlConnection connection = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = ConnectionDB.ConnectionSt;
           await ConnectionDB.OpenConnectAsync();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
                connection.Close();
        }

         async Task selectClient(string nametable)
         {            
             SqlDataReader datareader = null;
             SqlCommand command = new SqlCommand("select * from "+ nametable + "", connection);
             DataTable dt = new DataTable();
             try
             {
                 datareader = await command.ExecuteReaderAsync();
                 DataGrid_main.ColumnWidth = new DataGridLength(DataGrid_main.Width / datareader.FieldCount, DataGridLengthUnitType.Star);
                 dt.Load(datareader);
                 DataGrid_main.ItemsSource = dt.DefaultView;                
             }
             catch
             {
                 MessageBox.Show("При хапросе данных возникла ошибка. Проверьте соединение с интернетом");
             }
             finally
             {
                 if (datareader != null && !datareader.IsClosed)
                     datareader.Close();
             }
         }                           

        private async void ClientsLoad(object sender, MouseButtonEventArgs e) // выгрузка клиентов
        {
            await selectClient("Clients");           
            SelectorColorMenu();
            Brush bar = this.Resources["Select_menu_punkt"] as Brush;
            Clients_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

        async void ServecesSelect(object sender, MouseButtonEventArgs e) // выгрузка услуг
        {
            await selectClient("Services");
            SelectorColorMenu();
            Uslugi_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

        async void RoomSelect(object sender, MouseButtonEventArgs e) // выгрузка комнат
        {
            await selectClient("Rooms");
            SelectorColorMenu();
            Room_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

         async void RequestServicesSelect(object sender, MouseButtonEventArgs e)  // выгрузка заявок на услуги
        {
            await selectClient("ServicesRequest");
            SelectorColorMenu();
            Zayavki_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

         async void TransactionSelect(object sender, MouseButtonEventArgs e) // выгрузка транзакций
        {
            await selectClient("TransactionServices");
            SelectorColorMenu();
            Transact_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

        async void EmployeeSelect(object sender, MouseButtonEventArgs e) // выгрузка сотрудников
        {
            await selectClient("Employees");
            SelectorColorMenu();
            Sotrudn_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

        async void AmountSelect(object sender, MouseButtonEventArgs e) // выгрузка расходов
        {
            await selectClient("Amount");
            SelectorColorMenu();
            Rashod_punkt.Background = this.Resources["Select_menu_punkt"] as Brush;
        }

        void SelectorColorMenu()
        {
            foreach (ListBoxItem item in Menu_listbox.Items)
            {
                LinearGradientBrush bg = item.Background as LinearGradientBrush;
                if (bg != null && bg.GradientStops[1] == (this.Resources["Select_menu_punkt"] as LinearGradientBrush).GradientStops[1])
                {
                    item.Background = this.Resources["defaultColor_listitem"] as Brush;
                }
            }
        }

        ListBoxItem SearchSelectMenu()
        {
            foreach (ListBoxItem item in Menu_listbox.Items)
            {
                LinearGradientBrush bg = item.Background as LinearGradientBrush;
                if (bg != null && bg.GradientStops[1] == (this.Resources["Select_menu_punkt"] as LinearGradientBrush).GradientStops[1])
                {
                    return item;
                }
            }
            return null;
        }

        private void AddAny(object sender, RoutedEventArgs e)
        {
            string who = ((SearchSelectMenu().Content as StackPanel).Children[1] as TextBlock).Text;
            if (who == "Сотрудники")
            {
                AddEmployeeWindows empl = new AddEmployeeWindows();
                empl.Show();
            }
            else if (who == "Клиенты")
            {
                AddClientsWindow clien = new AddClientsWindow();
                clien.Show();
            }
            else if (who == "Заявки")
            {
                AddRequest clien = new AddRequest();
                clien.Show();
            }
        }
    }
}
