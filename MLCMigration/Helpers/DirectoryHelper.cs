using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace RibbonDemo02.Helpers
{
    public class DirectoryHelper
    {
        // Gibt die Struktur eines Verzeichnisses als eine hierarchische Liste von TreeViewItems zurück
        public static void LoadDirectoryStructure(string path, TreeView treeView)
        {
            treeView.Items.Clear();
            if (Directory.Exists(path))
            {
                var rootItem = new TreeViewItem { Header = new DirectoryInfo(path).Name, Tag = path, IsExpanded = true };
                treeView.Items.Add(rootItem);

                LoadSubDirectories(rootItem);
            }
            else
            {
                // Wenn der Pfad nicht existiert, könnte eine Nachricht im UI ausgegeben werden.
                throw new DirectoryNotFoundException($"Der angegebene Pfad existiert nicht: {path}");
            }
        }

        // Rekursive Funktion zum Laden von Unterverzeichnissen und Dateien
        private static void LoadSubDirectories(TreeViewItem parentItem)
        {
            string directoryPath = parentItem.Tag as string;

            if (directoryPath != null)
            {
                try
                {
                    // Lade Unterverzeichnisse
                    foreach (var directory in Directory.GetDirectories(directoryPath))
                    {
                        var directoryItem = new TreeViewItem
                        {
                            Header = new DirectoryInfo(directory).Name,
                            Tag = directory
                        };
                        parentItem.Items.Add(directoryItem);

                        // Rekursive Auffüllung für Sub-Directories
                        LoadSubDirectories(directoryItem);
                    }

                    // Lade Dateien
                    foreach (var file in Directory.GetFiles(directoryPath))
                    {
                        var fileItem = new TreeViewItem
                        {
                            Header = Path.GetFileName(file),
                            Tag = file
                        };
                        parentItem.Items.Add(fileItem);
                    }
                }
                catch (Exception ex)
                {
                    throw new IOException($"Fehler beim Laden des Verzeichnisses: {ex.Message}");
                }
            }
        }
    }
}
