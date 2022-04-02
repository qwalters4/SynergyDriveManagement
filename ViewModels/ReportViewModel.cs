using IM.Common;
using IM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IM.ViewModels
{
    class ReportViewModel : BaseViewModel
    {
        private ReportModel model;
        private string outputpath;
        private string poNumber;

        public string Outputpath { get => outputpath; set => outputpath = value; }
        public string PONumber { get => poNumber; set => poNumber = value; }

        public ReportViewModel()
        {
            Outputpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            model = new ReportModel();
        }

        public void POReportGen()
        {
            model.POReportGenerator();
        }

        public void DriveListReport()
        {
            if(!Int32.TryParse(PONumber, out var d))
            {
                PONumber = "";
                return;
            }
            model.DriveListReport(d, Outputpath);
        }
    }
}
