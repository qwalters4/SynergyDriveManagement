using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using IM.Common;
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

        public ObservableCollection<InventoryItem> Query() 
        {
            string query = "SELECT * FROM hdd";
            ObservableCollection<InventoryItem> list = new ObservableCollection<InventoryItem>();
            InventoryItem item = new InventoryItem();
            NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    item = new InventoryItem();
                    item.DiskInterface = (string)reader.GetValue(reader.GetOrdinal("connector"));
                    item.FormFactor = (string)reader.GetValue(reader.GetOrdinal("formfactor"));
                    item.Capacity = (int)reader.GetValue(reader.GetOrdinal("capacity"));
                    item.Brand = (string)reader.GetValue(reader.GetOrdinal("brand"));
                    item.ChangeType = DBChangeType.NoChange;

                    list.Add(item);
                }
            }
            return list;
        }
        
    }
}