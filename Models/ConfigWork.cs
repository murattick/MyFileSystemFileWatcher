using System.ComponentModel.DataAnnotations;

namespace MyFileSystemFileWatcher
{
    public static class ConfigWork
    {
        public static string WatchInboxPath { get; set; }
        public static string WatchOutboxPath { get; set; }
        public static string SuccessFolderPath { get; set; }
        public static string LogFilePath { get; set; }
        public static string LogErrorsFilePath { get; set; }
        public static string Database_connection { get; set; }
    }
}
