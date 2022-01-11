﻿using System;
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
            _connection.ConnectionString = "Host=65.26.61.201;Username=inventory;Password=whygod1234;Database=inventory";
            _connection.Open();
            //65.26.61.201 is the public ip
            //192.168.1.229 is the local ip
        }
        public bool InsertFailsafe(List<InventoryItem> incoming)
        {
            foreach (InventoryItem item in incoming)
            {
                string Query = "insert into hdd (brand, modelid, connector, formfactor, quantity, capacity ) values('" + item.Brand + "', '" + item.ModelID + "', '" + item.DiskInterface + "', '" + item.FormFactor + "', " + item.Quantity + ", " + item.Capacity +")on conflict(modelid) do update set ";
                Query += " brand = '" + item.Brand + "',connector = '" + item.DiskInterface + "',formfactor = '" + item.FormFactor + "',quantity = " + item.Quantity + ",capacity = " + item.Capacity + ";";
                NpgsqlCommand insert = new NpgsqlCommand(Query, _connection);
                insert.ExecuteNonQuery();
                Query = "";

            }
            return false;
        }
        public ObservableCollection<InventoryItem> Query(List<string> ff, List<string> conn, List<string> brand)
        {
            //basic format for sql query
            string query = "SELECT * FROM hdd";
            ObservableCollection<InventoryItem> list = new ObservableCollection<InventoryItem>();
            InventoryItem item = new InventoryItem();
            bool where = false;
            bool deleteAND = false;
            //if brand filter is null then skip
            if (brand.Count != 0)
            {
                where = true;
                deleteAND = true;
                query += " WHERE";
                foreach(string brandName in brand)
                {
                    query += " brand='" + brandName + "' OR ";
                }
                if (where == true)
                {
                    query = query.Substring(0, (query.Length - 3));
                    query += " AND ";
                }
            }
            //if formfactor filter is null then skip
            if (ff.Count != 0)
            {
                if (where == false) {
                    query += " WHERE";
                    where = true;
                    deleteAND = true;
                }
                foreach(string s in ff)
                {
                    query += " formfactor='" + s + "' OR ";
                }
                if (where == true)
                {
                    query = query.Substring(0, (query.Length - 3));
                    query += " AND ";
                }
            }
            //if capacity filter is null then skip

            //if connector filter is null then skip
            if (conn.Count != 0)
            {
                if (where == false)
                {
                    query += " WHERE";
                    where = true;
                }
                foreach(string s in conn)
                {
                    query += " connector='" + s + "' OR ";
                }
                if (where == true)
                    deleteAND = false;
                    query = query.Substring(0, (query.Length - 3));
            }
            if (deleteAND == true)
                query = query.Substring(0, (query.Length - 4));

            //filter 0 quantity items out by default
            if(deleteAND == false)
                query += " WHERE quantity != 0";
            else
                query += " AND quantity != 0";

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
                    item.ChangeType = DBChangeType.NoChange;

                    list.Add(item);
                }
            }
            return list;
        }

        public ObservableCollection<InventoryItem> Query() 
        {
            //basic format for sql query
            string query = "SELECT * FROM hdd";
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