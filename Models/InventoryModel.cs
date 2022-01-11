using IM.Common;
using IM.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            //dataservice.Update(incoming);
        }

        public void CloseConnection()
        {
            dataservice.CloseConnection();
        }

    }
}
