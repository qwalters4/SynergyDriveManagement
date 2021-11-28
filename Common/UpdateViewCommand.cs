using IM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IM.Common
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;
        private InventoryViewModel inventoryViewModel;
        private ReportViewModel reportViewModel;
        private SettingsViewModel settingsViewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
            inventoryViewModel = new InventoryViewModel();
            reportViewModel = new ReportViewModel();
            settingsViewModel = new SettingsViewModel();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Inventory")
            {
                viewModel.SelectedViewModel = inventoryViewModel;
            }
            else if (parameter.ToString() == "Report")
            {
                viewModel.SelectedViewModel = reportViewModel;
            }
            else if (parameter.ToString() == "Settings")
            {
                viewModel.SelectedViewModel = settingsViewModel;
            }
        }
    }
}
