using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IM.Common
{
    public class KVpair : INotifyPropertyChanged
    {
        private string key;
        private bool value;

        public KVpair(string s, bool v)
        {
            key = s;
            value = v;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.
        // The CallerMemberName attribute that is applied to the optional propertyName
        // parameter causes the property name of the caller to be substituted as an argument.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string Key
        {
            get => key;
            set
            {
                key = value;
                NotifyPropertyChanged();
            }
        }
        public bool test
        {
            get => value;
            set
            {
                this.value = value;
                NotifyPropertyChanged();
            }
        }

        override public string ToString()
        {
            return "{" + key + "," + value + "}";
        }
    }
}
