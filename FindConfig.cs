using System;
using System.IO;

namespace MyFileSystemFileWatcher
{
    public static class FindConfig
    {
        public static string ConfigWorkFilePath()
        {
            try
            {
                string FilePath = "";
                string[] fileEntries = Directory.GetFiles(@"C:\Users\aleksey.muratov\Source\repos\MyFileSystemFileWatcher\Conf\", "ConfigEDI.xml", SearchOption.TopDirectoryOnly);

                foreach (string fileName in fileEntries)
                    FilePath = fileName;
                return FilePath;
            }
            catch(Exception ex)
            {
                AppLog.WriteErrorMessage("Ошибка при определении пути РАБОЧЕГО конфигурационного файла. Исключение: " +  ex.Message);
                return "";                
            }
        }

        public static string ConfigTestFilePath()
        {
            try
            {
                string FilePath = "";
                string[] fileEntries = Directory.GetFiles(@"C:\Users\aleksey.muratov\Source\repos\MyFileSystemFileWatcher\Conf\", "ConfigEDI_TEST.xml", SearchOption.TopDirectoryOnly);

                foreach (string fileName in fileEntries)
                    FilePath = fileName;
                return FilePath;
            }
            catch (Exception ex)
            {
                AppLog.WriteErrorMessage("Ошибка при определении пути ТЕСТОВОГО конфигурационного файла. Исключение: " + ex.Message);
                return "";
            }
        }
    }
}
