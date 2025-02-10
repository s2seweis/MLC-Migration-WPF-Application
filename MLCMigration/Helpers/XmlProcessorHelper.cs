using RibbonDemo02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace RibbonDemo02.Helpers
{
    public class XmlProcessorHelper
    {
        // Convert XML with progress reporting using IProgress<ProgressUpdate>
        public List<XMLData> ConvertXML(
            string file,
            string nodePrefix,
            int fileAmount,
            string parentFolder,
            string backupFolder,
            IProgress<ProgressUpdate> progress)
        {
            FileHelper fileHelper = new FileHelper();

            if (!fileHelper.CheckIfFileFollowsPattern(file))
            {
                return null; // Return early if the file doesn't match the pattern
            }

            List<XMLData> convertedXMLData = new List<XMLData>();
            XmlDocument xMLDocument = new XmlDocument();

            try
            {
                xMLDocument.Load(file);
                //fileHelper.showProgress(fileAmount, "case2", nodePrefix, parentFolder, backupFolder);

                // Get total child nodes for progress tracking
                int totalNodes = xMLDocument.DocumentElement.ChildNodes.Count;
                int processedNodes = 0;

                // Variable to track the last reported progress percentage.
                double lastReportedPercentage = 0;

                // Loop through the child nodes
                foreach (XmlNode node in xMLDocument.DocumentElement.ChildNodes)
                {
                    var xmlData = GetListOfXMLData(node, file, nodePrefix);
                    if (!string.IsNullOrEmpty(xmlData.Id))
                    {
                        convertedXMLData.Add(xmlData);
                    }

                    processedNodes++;
                    double progressPercentage = (double)processedNodes / totalNodes * 100;

                    // Only report progress if it has increased by at least 1% since the last report.
                    if (progressPercentage - lastReportedPercentage >= 1.0 || processedNodes == totalNodes)
                    {
                        lastReportedPercentage = progressPercentage;

                        //var progressUpdate = new ProgressUpdate
                        //{
                        //    Percent = progressPercentage,
                        //    FilesProcessed = processedNodes, // Here, "FilesProcessed" represents nodes processed
                        //    TotalFiles = totalNodes
                        //};

                        //progress.Report(progressUpdate);
                    }
                    // Artificial delay (e.g., 10 milliseconds)
                    System.Threading.Thread.Sleep(2);
                }
            }
            catch (Exception ex)
            {
                LoggerHelper.Log($"Error processing file {file}: {ex.Message}");
                return null;
            }

            return convertedXMLData;
        }

        public XMLData GetListOfXMLData(XmlNode node, string file, string nodePrefix)
        {
            XMLData xMLData = new XMLData();

            DirectoryInfo parentDir = Directory.GetParent(file);
            xMLData.Language_Id = Directory.GetParent(parentDir.ToString()).Name;

            xMLData.Id = node[nodePrefix + "_id"]?.InnerXml;
            xMLData.App_id = Path.GetFileNameWithoutExtension(file);
            xMLData.Caption = node[nodePrefix + "_caption"]?.InnerXml;
            xMLData.Tooltip = node[nodePrefix + "_tooltip"]?.InnerXml;
            xMLData.Title = node[nodePrefix + "_title"]?.InnerXml;
            xMLData.Text = node[nodePrefix + "_text"]?.InnerXml;
            xMLData.Type = node[nodePrefix + "_typ"]?.InnerXml;
            xMLData.Verweis_Id = node[nodePrefix + "_verweisid"]?.InnerXml;
            xMLData.Laufnummer = node[nodePrefix + "_lfdnr"]?.InnerXml;
            xMLData.Startwert = node[nodePrefix + "_lfdnr_offset"]?.InnerXml;
            xMLData.Textabweichung = node[nodePrefix + "_lfdnr_offset"]?.InnerXml;
            xMLData.Bearbeitung = node[nodePrefix + "_handling"]?.InnerXml;

            return xMLData;
        }
    }
}
