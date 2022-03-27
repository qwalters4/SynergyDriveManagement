using IM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IM.Views
{
    /// <summary>
    /// Interaction logic for ReportView.xaml
    /// </summary>
    public partial class ReportView : UserControl
    {
        private ReportViewModel viewModel { get { return (ReportViewModel)DataContext; } }

        public ReportView()
        {
            InitializeComponent();
        }

        private void OpenReportGen(object sender, RoutedEventArgs e)
        {
            viewModel.POReportGen();
        }
    }
}
