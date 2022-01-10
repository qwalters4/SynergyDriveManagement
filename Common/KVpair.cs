using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IM.Common
{
    class KVpair : INotifyPropertyChanged
    {
        private string key;
        private bool value;

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
        public bool Value
        {
            get => value;
            set
            {
                this.value = value;
                NotifyPropertyChanged();
            }
        }
    }
}
