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
            //basic format for sql query
            string query = "SELECT * FROM hdd";
            ObservableCollection<InventoryItem> list = new ObservableCollection<InventoryItem>();
            InventoryItem item = new InventoryItem();

            //sum all of the filtering list together to see if we need to filter

            //if the summed filter is empty run basic query

            //if the filter is not null then run through each list and apply the correct filter

            //if brand filter is null then skip

            //if formfactor filter is null then skip

            //if capacity filter is null then skip

            //if connector filter is null then skip

            //execute sql query with the given connection
            NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);
            //read results from query and add to item class
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
        public List<string> BrandQuery()
        {
            List<string> list = new List<string>();
            //run query to get all brand names to pass to the interface
            string brandquery = "SELECT brand FROM hdd";
            NpgsqlCommand BrandQuery = new NpgsqlCommand(brandquery, _connection);
            //read results from brandquery and add to brand list
            using (var reader = BrandQuery.ExecuteReader())
            {
                while (reader.Read())
                {
                    string temp;
                    temp = (string)reader.GetValue(reader.GetOrdinal("brand"));
                    list.Add(temp);
                }
            }
            return list;
        }
    }
}