using System;
using System.Xml;

namespace MyFileSystemFileWatcher
{
    public static class ReadConfig
    {        
        public static bool ReadWorkConfig(string path)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(path);
                XmlNodeList watch_dirs = xml.GetElementsByTagName("watch_path");
                XmlNodeList success_dir = xml.GetElementsByTagName("config");
                XmlNodeList log_dir = xml.GetElementsByTagName("log");

                foreach (XmlElement param in watch_dirs)
                {
                    ConfigWork.WatchInboxPath = param.GetElementsByTagName("inbox").Item(0).InnerText;
                    ConfigWork.WatchOutboxPath = param.GetElementsByTagName("outbox").Item(0).InnerXml;
                }

                foreach (XmlElement param in success_dir)
                {
                    ConfigWork.SuccessFolderPath = param.GetElementsByTagName("success_path").Item(0).InnerText;
                }

                foreach (XmlElement param in log_dir)
                {
                    ConfigWork.LogFilePath = param.GetElementsByTagName("all").Item(0).InnerText;
                    ConfigWork.LogErrorsFilePath = param.GetElementsByTagName("errors").Item(0).InnerXml;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при чтении РАБОЧЕГО конфигурационнного файла. Исключение: " + ex.Message);
                AppLog.WriteErrorMessage("Ошибка при чтении РАБОЧЕГО конфигурационнного файла. Исключение: " + ex.Message);
                return false;
            }
        }
        public static bool ReadTestConfig(string path)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка при чтении ТЕСТОВОГО конфигурационнного файла. Исключение: " + ex.Message);
                AppLog.WriteErrorMessage("Ошибка при чтении ТЕСТОВОГО конфигурационнного файла. Исключение: " + ex.Message);
                return false;
            }           
        }
    }
}
