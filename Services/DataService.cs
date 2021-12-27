using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Npgsql;

namespace IM.Services
{

    public class DataService
    {
        private NpgsqlConnection _connection;

        public DataService()
        {
            _connection = new NpgsqlConnection();
            _connection.ConnectionString = "Host=192.168.1.229;Username=inventory;Password=whygod1234;Database=inventory";
            _connection.Open();
        }

        public List<string> Query() 
        {
            List<string> list = new List<string>();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM hdd", _connection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    list.Add((string) reader.GetValue(reader.GetOrdinal("connector")));
            }
            return list;
        }
    }
}
