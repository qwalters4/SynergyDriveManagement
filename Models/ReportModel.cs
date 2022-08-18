using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IM.Common;
using Newtonsoft.Json;

namespace IM.Models
{
    class ReportModel
    {
        public void POReportGenerator()
        {
            System.Diagnostics.Process.Start(Application.StartupPath.ToString() + @"\POReportGenerator.exe");
        }

        public void DriveListReport(int po, string output)
        {
            if (Directory.Exists(Path.Combine("\\\\TRUENAS\\DiskTesting\\Archive\\", po.ToString())))
            {
                List<PhysicalDisk> pending = new List<PhysicalDisk>();

                foreach (string file in Directory.EnumerateFiles(Path.Combine("\\\\TRUENAS\\DiskTesting\\Archive\\", po.ToString()), "*.json"))
                {
                    PhysicalDisk temp = JsonConvert.DeserializeObject<PhysicalDisk>(File.ReadAllText(file));
                    pending.Add(temp);
                }

                List<string> report = new List<string>();
                foreach(PhysicalDisk temp in pending)
                {
                    report.Add(temp.ToString());
                }

                using (TextWriter tw = new StreamWriter(Path.Combine(output, (po.ToString() + "_DriveReport.txt"))))
                {
                    foreach (String s in report)
                        tw.WriteLine(s);
                }
            }
        }

    }
}
