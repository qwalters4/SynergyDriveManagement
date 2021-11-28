using IM.Common;
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
        private string outputpath;

        public string Outputpath { get => outputpath; set => outputpath = value; }

        public ReportViewModel()
        {
            Outputpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}
