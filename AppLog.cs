using System;
using System.IO;

namespace MyFileSystemFileWatcher
{
    public class AppLog
    {
        static object obj = new object();
        public static void WriteMessage(string msg, string fileName = null, string fileEvent = null, string fullPath = null)
        {
            try
            {
                if(string.IsNullOrEmpty(fileName) && string.IsNullOrEmpty(fileEvent) && string.IsNullOrEmpty(fullPath) && !string.IsNullOrEmpty(msg))
                {                    
                    lock (obj)
                    {
                        using (StreamWriter writer = new StreamWriter(ConfigWork.LogFilePath, true))
                        {
                            writer.WriteLine(String.Format("{0} {1}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), msg));
                            writer.Flush();
                            writer.Dispose();
                        }
                    }
                }
                else
                {
                    lock (obj)
                    {
                        using (StreamWriter writer = new StreamWriter(ConfigWork.LogFilePath, true))
                        {
                            writer.WriteLine(String.Format("{0} Файл: {1} был {2}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), fileName, fileEvent));
                            writer.Flush();
                            writer.Dispose();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка при записи лога. Исключение: " + ex.Message);
                WriteErrorMessage("Ошибка при записи лога. Исключение: " + ex.Message);
            }
            
        }
        public static void WriteErrorMessage(string msg)
        {
            try
            {
                lock (obj)
                {
                    using (StreamWriter writer = new StreamWriter(ConfigWork.LogErrorsFilePath, true))
                    {
                        writer.WriteLine(String.Format("{0} {1}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), msg));
                        writer.Flush();
                        writer.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка при записи лога ошибок. Исключение: ");
            }
        }       
    }
}
