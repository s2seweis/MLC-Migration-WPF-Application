using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RibbonDemo02.Models
{
    public class ProgressUpdate
    {
        public double Percent { get; set; }
        public int FilesProcessed { get; set; }
        public int TotalFiles { get; set; }
        public int CurrentLanguage { get; set; }
        public string CurrentFolderNew { get; set; }
    }
}
