using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDbClients
{
    static class User
    {



    }

    public static class ConnectionDB
    {

        public static SqlConnection ConnectionSt { get;  }

        static ConnectionDB()
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["hotel_My"].ConnectionString;
            ConnectionSt = new SqlConnection(connectionstring);            
        }

        public async static Task OpenConnectAsync()
        {
            await ConnectionSt.OpenAsync();
        }

        public static void CloseConnect()
        {
             ConnectionSt.Close();
        }
    }


}
