using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
             _connection.ConnectionString = "Host=65.26.61.201;Username=inventory;Password=whygod1234;Database=inventory;Timeout=3";
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cannot connect to Database, please contact you local administrator.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            //65.26.61.201 is the public ip
            //192.168.1.229 is the local ip
        }

        public bool assertConnection()
        {
            if (_connection.Host != null)
                return true;
            else return false;
        }
        public void InsertFailsafe(List<InventoryItem> incoming)
        {
            if (!assertConnection()) return;

            foreach (InventoryItem item in incoming)
            {
                string Query = "insert into hdd (brand, modelid, connector, formfactor, quantity, capacity, lastupdatetime ) values('" + item.Brand + "', '" + item.ModelID + "', '" + item.DiskInterface + "', '" + item.FormFactor + "', " + item.Quantity + ", " + item.Capacity + ", " + "localtimestamp" +")on conflict on constraint hdd_un do update set";
                Query += " brand = '" + item.Brand + "',connector = '" + item.DiskInterface + "',formfactor = '" + item.FormFactor + "',quantity = " + item.Quantity + ",capacity = " + item.Capacity + ", lastupdatetime = localtimestamp;";
                NpgsqlCommand insert = new NpgsqlCommand(Query, _connection);
                insert.ExecuteNonQuery();
                Query = "";
            }
        }
        public void Update(List<InventoryItem> incoming)
        {
            if (!assertConnection()) return;
            
            foreach (InventoryItem incomingItem in incoming)
            {
                string updatestring = "Update hdd set brand = '" + incomingItem.Brand + "' , modelid = '" + incomingItem.ModelID + "', connector = '" + incomingItem.DiskInterface + "', formfactor = '" + incomingItem.FormFactor + "', quantity = " + incomingItem.Quantity + ", capacity = " + incomingItem.Capacity + ", lastupdatetime = localtimestamp" + " WHERE id = " + incomingItem.UniqueID + ";";
                NpgsqlCommand rowupdate = new NpgsqlCommand(updatestring, _connection);
                rowupdate.ExecuteNonQuery();
                updatestring = "";
            }
        }
        public ObservableCollection<InventoryItem> Query(List<string> ff, List<string> conn, List<string> brand, int caplower, int capupper, bool quantitycheck)
        {
            if (!assertConnection()) return new ObservableCollection<InventoryItem>();

            //basic format for sql query
            string query = "select * from (select * from ( select * from(select *from(select * from hdd ) as quant where";
            List<InventoryItem> list = new List<InventoryItem>();
            InventoryItem item = new InventoryItem();
            //if brand filter is null then skip
            if (brand.Count != 0)
            {
                foreach (string brandName in brand)
                {
                    query += " brand='" + brandName + "' OR ";
                }
                query = query.Substring(0, query.Length - 3);
                query += ") as manu where";
            }
            else
            {
                query += " brand = brand ) as manu where";
            }
            //if connector filter is null then skip
            if (conn.Count != 0)
            {
                foreach (string s in conn)
                {
                    query += " connector='" + s + "' OR ";
                }
                query = query.Substring(0, query.Length - 3);
                query += ") as con where";
            }
            else
            {
                query += " connector = connector ) as con where";
            }
            //add capacity filter

            query += " capacity BETWEEN " + caplower + " AND " + capupper + " AND ";

            //if formfactor filter is null then skip
            if (ff.Count != 0)
                {
                    foreach(string s in ff)
                {
                    query += " formfactor='" + s + "' OR ";
                }
                    query = query.Substring(0, query.Length - 3);
                    query += ") as ff;";
                }
            else 
                { 
                query += " formfactor = formfactor) as ff;";
                }

            //execute sql query with the given connection
            NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);

            //read results from query and add to item class
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    item = new InventoryItem();
                    item.ModelID = (string)reader.GetValue(reader.GetOrdinal("modelid"));
                    item.DiskInterface = (string)reader.GetValue(reader.GetOrdinal("connector"));
                    item.FormFactor = (string)reader.GetValue(reader.GetOrdinal("formfactor"));
                    item.Capacity = (int)reader.GetValue(reader.GetOrdinal("capacity"));
                    item.Brand = (string)reader.GetValue(reader.GetOrdinal("brand"));
                    item.Quantity = (int)reader.GetValue(reader.GetOrdinal("quantity"));
                    item.UniqueID = (int)reader.GetValue(reader.GetOrdinal("id"));
                    item.ChangeType = DBChangeType.NoChange;
                    list.Add(item);
                }
            }

            //Filter list by quantity of 0
            if (!quantitycheck)
                list = list.AsParallel().Where(x => x.Quantity != 0).ToList();

            ObservableCollection<InventoryItem> outgoing = new ObservableCollection<InventoryItem>(list);

            return outgoing;
        }

        public ObservableCollection<InventoryItem> Query() 
        {
            if (!assertConnection()) return new ObservableCollection<InventoryItem>();

            //basic format for sql query
            string query = "SELECT * FROM hdd WHERE quantity != 0;";
            ObservableCollection<InventoryItem> list = new ObservableCollection<InventoryItem>();
            InventoryItem item = new InventoryItem();

            //execute sql query with the given connection
            NpgsqlCommand cmd = new NpgsqlCommand(query, _connection);
            //read results from query and add to item class
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    item = new InventoryItem();
                    item.ModelID = (string)reader.GetValue(reader.GetOrdinal("modelid"));
                    item.DiskInterface = (string)reader.GetValue(reader.GetOrdinal("connector"));
                    item.FormFactor = (string)reader.GetValue(reader.GetOrdinal("formfactor"));
                    item.Capacity = (int)reader.GetValue(reader.GetOrdinal("capacity"));
                    item.Brand = (string)reader.GetValue(reader.GetOrdinal("brand"));
                    item.Quantity = (int)reader.GetValue(reader.GetOrdinal("quantity"));
                    item.UniqueID = (int)reader.GetValue(reader.GetOrdinal("id"));
                    item.ChangeType = DBChangeType.NoChange;

                    list.Add(item);
                }
            }
            return list;
        }
        public List<string> BrandQuery()
        {
            if (!assertConnection()) return new List<string>();

            List<string> list = new List<string>();
            //run query to get all brand names to pass to the interface
            string brandquery = "SELECT Distinct brand FROM hdd";
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
        public List<string> FormFactorQuery()
        {
            if (!assertConnection()) return new List<string>();

            List<string> list = new List<string>();
            //run query to get all brand names to pass to the interface
            string formfactorquery = "SELECT Distinct formfactor FROM hdd";
            NpgsqlCommand FormFactorQuery = new NpgsqlCommand(formfactorquery, _connection);
            //read results from brandquery and add to brand list
            using (var reader = FormFactorQuery.ExecuteReader())
            {
                while (reader.Read())
                {
                    string temp;
                    temp = (string)reader.GetValue(reader.GetOrdinal("formfactor"));
                    list.Add(temp);
                }
            }
            return list;
        }
        public List<string> ConnectorQuery()
        {
            if (!assertConnection()) return new List<string>();

            List<string> list = new List<string>();
            //run query to get all brand names to pass to the interface
            string connectorquery = "SELECT Distinct connector FROM hdd";
            NpgsqlCommand ConnectorQuery = new NpgsqlCommand(connectorquery, _connection);
            //read results from brandquery and add to brand list
            using (var reader = ConnectorQuery.ExecuteReader())
            {
                while (reader.Read())
                {
                    string temp;
                    temp = (string)reader.GetValue(reader.GetOrdinal("connector"));
                    list.Add(temp);
                }
            }
            return list;
        }

        public void CloseConnection()
        {
            _connection.Close();
        }
    }
}