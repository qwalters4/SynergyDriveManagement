using IM.Common;
using IM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Models
{
    class InventoryModel
    {
        private DataService dataservice;

        public DataService DataService { get => dataservice; set => dataservice = value; }

        public ObservableCollection<KVpair> GetBrandList()
        {
            List<string> temp = dataservice.BrandQuery();
            ObservableCollection<KVpair> o = new ObservableCollection<KVpair>();
            KVpair t;

            foreach (string s in temp)
            {
                t = new KVpair(s, false);
                o.Add(t);
            }

            return o;
        }

        public ObservableCollection<KVpair> GetFFList()
        {
            List<string> temp = dataservice.FormFactorQuery();
            ObservableCollection<KVpair> o = new ObservableCollection<KVpair>();
            KVpair t;

            foreach (string s in temp)
            {
                t = new KVpair(s, false);
                o.Add(t);
            }

            return o;
        }

        public ObservableCollection<KVpair> GetConnectorList()
        {
            List<string> temp = dataservice.ConnectorQuery();
            ObservableCollection<KVpair> o = new ObservableCollection<KVpair>();
            KVpair t;

            foreach (string s in temp)
            {
                t = new KVpair(s, false);
                o.Add(t);
            }

            return o;
        }

        public void InsertRows(List<InventoryItem> incoming)
        {
            dataservice.InsertFailsafe(incoming);
        }

        public void UpdateRows(List<InventoryItem> incoming)
        {
            dataservice.Update(incoming);
        }

        public void ExportCsv<T>(List<T> genericList, string fileName)
        {
            var sb = new StringBuilder();
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var finalPath = Path.Combine(basePath, fileName + ".csv");
            var header = "";
            var info = typeof(T).GetProperties();
            if (!File.Exists(finalPath))
            {
                var file = File.Create(finalPath);
                file.Close();
                foreach (var prop in typeof(T).GetProperties())
                {
                    header += prop.Name + ", ";
                }
                header = header.Substring(0, header.Length - 2);
                sb.AppendLine(header);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
            foreach (var obj in genericList)
            {
                sb = new StringBuilder();
                var line = "";
                foreach (var prop in info)
                {
                    line += prop.GetValue(obj, null) + ", ";
                }
                line = line.Substring(0, line.Length - 2);
                sb.AppendLine(line);
                TextWriter sw = new StreamWriter(finalPath, true);
                sw.Write(sb.ToString());
                sw.Close();
            }
        }

        public void CloseConnection()
        {
            dataservice.CloseConnection();
        }

    }
}
