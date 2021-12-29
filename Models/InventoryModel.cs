using IM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Models
{
    class InventoryModel
    {
        private DataService dataservice;

        public DataService DataService { get => dataservice; set => dataservice = value; }

        public List<KeyValuePair<string, bool>> GetBrandList()
        {
            
        }

    }
}
