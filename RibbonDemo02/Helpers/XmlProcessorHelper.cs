using RibbonDemo02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace RibbonDemo02.Helpers
{
    public class XmlProcessorHelper
    {
        // Convert XML with progress reporting
        public List<XMLData> ConvertXML(string file, string nodePrefix, int fileAmount, string parentFolder, string backupFolder, IProgress<double> progress)
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
                fileHelper.showProgress(fileAmount, "case2", nodePrefix, parentFolder, backupFolder); // Call showProgress after loading the file

                // Get total child nodes for progress tracking
                int totalNodes = xMLDocument.DocumentElement.ChildNodes.Count;
                int processedNodes = 0;

                // Loop through the child nodes
                foreach (XmlNode node in xMLDocument.DocumentElement.ChildNodes) // Use DocumentElement for root node
                {
                    var xmlData = GetListOfXMLData(node, file, nodePrefix);
                    if (!string.IsNullOrEmpty(xmlData.Id)) // Check if Id is not null or empty
                    {
                        convertedXMLData.Add(xmlData);
                    }

                    // Update progress
                    processedNodes++;
                    double progressPercentage = (double)processedNodes / totalNodes * 100;
                    progress.Report(progressPercentage); // Report progress after each node is processed


                }
            }
            catch (Exception ex)
            {
                // Handle the error (you can log it if needed)
                LoggerHelper.Log($"Error processing file {file}: {ex.Message}");
                return null; // Return null if there's an issue loading or parsing the XML
            }

            return convertedXMLData;
        }

        // Extract XML data from each node
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
