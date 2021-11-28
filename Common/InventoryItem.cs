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
        private string model_id;
        private string disk_interface;
        private string form_factor;
        private string rotation_rate;
        private int sector_size;
        private int po;

        public int PO
        {
            get
            {
                return po;
            }
            set
            {
                if (Set(ref po, value))
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
        public string RotationRate
        {
            get
            {
                return rotation_rate;
            }
            set
            {
                if (Set(ref rotation_rate, value))
                {
                    SetChanged();
                }
            }
        }
        public int SectorSize
        {
            get
            {
                return sector_size;
            }
            set
            {
                if (Set(ref sector_size, value))
                {
                    SetChanged();
                }
            }
        }


    }
}
