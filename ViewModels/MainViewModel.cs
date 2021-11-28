using IM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IM.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedviewmodel = new InventoryViewModel();
        public BaseViewModel SelectedViewModel
        {
            get
            {
                return _selectedviewmodel;
            }
            set
            {
                _selectedviewmodel = value; OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
