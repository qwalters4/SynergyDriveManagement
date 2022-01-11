using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.Common
{
    public class InventoryItem : DbObservableObject
    {
        private string brand;
        private string model_id;
        private string disk_interface;
        private string form_factor;
        private int size;
        private int quantity;

        public InventoryItem()
        {
            ChangeType = DBChangeType.Insert;
            Brand = "<Brand>";
            ModelID = "<ModelID>";
            DiskInterface = "<DiskInterface>";
            FormFactor = "<FormFactor>";
            Capacity = 0;
        }

        public string Brand
        {
            get
            {
                return brand;
            }
            set
            {
                if (Set(ref brand, value))
                {
                    SetChanged();
                }
            }
        }

        public string ModelID
        {
            get
            {
                return model_id;
            }
            set
            {
                if (Set(ref model_id, value))
                {
                    SetChanged();
                }
            }
        }
        public string DiskInterface
        {
            get
            {
                return disk_interface;
            }
            set
            {
                if (Set(ref disk_interface, value))
                {
                    SetChanged();
                }
            }
        }
        public string FormFactor
        {
            get
            {
                return form_factor;
            }
            set
            {
                if (Set(ref form_factor, value))
                {
                    SetChanged();
                }
            }
        }
        public int Capacity
        {
            get
            {
                return size;
            }
            set
            {
                if (Set(ref size, value))
                {
                    SetChanged();
                }
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                if (Set(ref quantity, value))
                {
                    SetChanged();
                }
            }
        }

    }
}
