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
        private BaseViewModel _selectedviewmodel = new BaseViewModel();
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
        public UpdateViewCommand UpdateView { get; set; }

        public MainViewModel()
        {
            UpdateView = new UpdateViewCommand(this);
            UpdateViewCommand = UpdateView;
            UpdateViewCommand.Execute("Inventory");
        }
    }
}
