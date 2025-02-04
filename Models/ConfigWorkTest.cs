namespace MyFileSystemFileWatcher
{
    public static class ConfigWorkTest
    {
        public static bool IsActive { get; set; }
        public static string WatchInboxPath { get; set; }
        public static string WatchOutboxPath { get; set; }
        public static string MoveInboxFilePath { get; set; }
        public static string MoveOutboxFilePath { get; set; }
        public static string LogFilePath { get; set; }
        public static string LogErrorsFilePath { get; set; }
        public static string Database_connection { get; set; }
    }
}
