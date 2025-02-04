using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RibbonDemo02.Helpers;
using RibbonDemo02.Models;

namespace RibbonDemo02.Service
{
    public class XmlProcessorService
    {
        private readonly XmlProcessorHelper _xmlHelper;

        public XmlProcessorService()
        {
            _xmlHelper = new XmlProcessorHelper();
        }

        public List<XMLData> ConvertXML(string file, string nodePrefix, int fileAmount, string parentFolder, string backupFolder, IProgress<double> progress)
        {
            return _xmlHelper.ConvertXML(file, nodePrefix, fileAmount, parentFolder, backupFolder, progress);
        }
    }
}
