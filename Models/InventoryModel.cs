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

        public ObservableCollection<KeyValuePair<string, bool>> GetBrandList()
        {
            List<string> temp = dataservice.BrandQuery();
            ObservableCollection<KeyValuePair<string, bool>> o = new ObservableCollection<KeyValuePair<string, bool>>();
            KeyValuePair<string, bool> t;

            foreach (string s in temp)
            {
                t = new KeyValuePair<string, bool>(s, false);
                o.Add(t);
            }

            return o;
        }

        public ObservableCollection<KeyValuePair<string, bool>> GetFFList()
        {
            List<string> temp = dataservice.FormFactorQuery();
            ObservableCollection<KeyValuePair<string, bool>> o = new ObservableCollection<KeyValuePair<string, bool>>();
            KeyValuePair<string, bool> t;

            foreach (string s in temp)
            {
                t = new KeyValuePair<string, bool>(s, false);
                o.Add(t);
            }

            return o;
        }

        public ObservableCollection<KeyValuePair<string, bool>> GetConnectorList()
        {
            List<string> temp = dataservice.ConnectorQuery();
            ObservableCollection<KeyValuePair<string, bool>> o = new ObservableCollection<KeyValuePair<string, bool>>();
            KeyValuePair<string, bool> t;

            foreach (string s in temp)
            {
                t = new KeyValuePair<string, bool>(s, false);
                o.Add(t);
            }

            return o;
        }

    }
}
