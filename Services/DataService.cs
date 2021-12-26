using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace IM.Services
{

    class DataService
    {
        private NpgsqlConnection _connection;

        public DataService()
        {
            _connection = new NpgsqlConnection();
            _connection.ConnectionString = "Host=192.168.1.229;Username=inventory;Password=whygod1234;Database=inventory";
        
        }

        public List<string> Query() 
        {
            List<string> list = new List<string>();
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM inventory", _connection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    list.Add(reader.GetString(0));
            }
            return list;
        }
    }
}
