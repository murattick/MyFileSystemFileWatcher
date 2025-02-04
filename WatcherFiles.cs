using System;
using System.IO;
using System.Security.Permissions;
using System.Threading;

namespace MyFileSystemFileWatcher
{
    public class WatcherFiles
    {
        private static bool Enabled { get; set; }
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void RunInbox(bool enabled, string path = null)
        {
            Enabled = enabled;

            if (string.IsNullOrEmpty(path))
            {
                Enabled = false;
            }
            else
            {
                // Create a new FileSystemWatcher and set its properties.
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    watcher.Path = path;
                    // the renaming of files or directories.
                    watcher.NotifyFilter = NotifyFilters.LastAccess
                                         | NotifyFilters.LastWrite
                                         | NotifyFilters.FileName
                                         | NotifyFilters.DirectoryName;

                    // Only watch text files.
                    //watcher.Filter = "*.txt";
                    Console.WriteLine("Просматриваемая директория: " + watcher.Path.ToString());
                    WriteLog("Просматриваемая директория: " + watcher.Path.ToString());
                    // Add event handlers.
                    watcher.Created += OnChanged;

                    // Begin watching.
                    watcher.EnableRaisingEvents = true;

                    // Wait for the user to quit the program.

                    while (Enabled)
                    {
                        Thread.Sleep(1000);
                    }

                }
            }

        }
        
        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void RunOutbox(bool enabled, string path = null)
        {
            Enabled = enabled;

            if (string.IsNullOrEmpty(path))
            {
                Enabled = false;
            }
            else
            {
                // Create a new FileSystemWatcher and set its properties.
                using (FileSystemWatcher watcher = new FileSystemWatcher())
                {
                    watcher.Path = path;
                    // the renaming of files or directories.
                    watcher.NotifyFilter = NotifyFilters.LastAccess
                                         | NotifyFilters.LastWrite
                                         | NotifyFilters.FileName
                                         | NotifyFilters.DirectoryName;

                    // Only watch text files.
                    //watcher.Filter = "*.txt";
                    Console.WriteLine("Просматриваемая директория: " + watcher.Path.ToString());
                    WriteLog("Просматриваемая директория: " + watcher.Path.ToString());
                    // Add event handlers.
                    watcher.Created += OnChanged;

                    // Begin watching.
                    watcher.EnableRaisingEvents = true;

                    // Wait for the user to quit the program.

                    while (Enabled)
                    {
                        Thread.Sleep(1000);
                    }

                }
            }

        }
        private static void OnChanged(object source, FileSystemEventArgs e)
        {            
            Console.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + $" File: {e.FullPath} {e.ChangeType}");
            AppLog.WriteMessage("", e.Name, e.ChangeType.ToString(), e.FullPath);
            string folder = GetFolderName.GetName(e.FullPath);
            switch (folder)
            {
                case var data when data == "inbox" :
                    Execute.GetIncomingMessage(e);
                    break;
                //case var data when data == "outbox" :
                //    MoveSuccessFiles.MoveFile(e.FullPath, ConfigWork.SuccessFolderPath, data, e.Name);
                //    Thread.Sleep(2000);
                //    break;
            }
            
        }
        private static void WriteLog(string msg)
        {
            AppLog.WriteMessage(msg);
        }
    }
}
