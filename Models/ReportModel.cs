using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IM.Models
{
    class ReportModel
    {
        public void POReportGenerator()
        {
            System.Diagnostics.Process.Start(Application.StartupPath.ToString() + @"\POReportGen.exe");
        }

    }
}
