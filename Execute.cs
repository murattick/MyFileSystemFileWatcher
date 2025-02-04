using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyFileSystemFileWatcher.Models;

namespace MyFileSystemFileWatcher
{
    public static class Execute
    {
        private static ExchangeEntities db;
        public static void GetIncomingMessage(FileSystemEventArgs e)
        {
            try
            {
                string file_body = "File reading error";
                string file_name = e.Name;
                string incoming_file = e.FullPath;
                int i = 1;

                using(StreamReader streamReader = new StreamReader(incoming_file))
                {
                    do
                    {
                        try
                        {
                            file_body = streamReader.ReadToEnd();
                            i = 0;
                        }
                        catch(Exception ex)
                        {
                            i += 1;
                            Console.WriteLine("Ошибка при чтении файла. Исключение: " + ex.Message);
                            AppLog.WriteErrorMessage("Ошибка при чтении файла. Исключение: " + ex.Message);
                            Thread.Sleep(500);
                        }
                        streamReader.Close();
                        streamReader.Dispose();
                    }
                    while (i > 0 && i < 20);

                    try
                    {
                        //Task task = new Task(() => db.procGetIncomingMessages(file_name, file_body));
                        //task.Start();
                        //task.Wait();
                        db = new ExchangeEntities();
                        db.procGetInboxMessage_test(file_name, file_body);

                        Console.WriteLine("Файл " + file_name + " был успешно передан в БД");
                        AppLog.WriteMessage("Файл " + file_name + " был успешно передан в БД");
                        MoveSuccessFiles.MoveFile(e.FullPath, ConfigWork.SuccessFolderPath, GetFolderName.GetName(e.FullPath), e.Name);
                        Thread.Sleep(1000);
                    }
                    catch(Exception ex)
                    {
                        AppLog.WriteMessage("ОШИБКА! Файл " + file_name + " небыл успешно передан в БД. Подробности в логе ошибок");
                        AppLog.WriteErrorMessage("ОШИБКА! Файл " + file_name + " небыл успешно передан в БД. Исключение: " + ex.Message);
                        Console.WriteLine("ОШИБКА! Файл " + file_name + " небыл успешно передан в БД. Исключение: " + ex.Message);
                    }
                }                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка в методе GetIncomingMessage. Исключение: " + ex.Message);
                AppLog.WriteErrorMessage("Ошибка в методе GetIncomingMessage. Исключение: " + ex.Message);
            }
        }
        public static void GetOutcomingMessage()
        {
            try
            {
                try
                {
                    var results = db.procGetOutboxMessage_test().ToList();
                    foreach(var data in results)
                    {
                        File.WriteAllText(ConfigWork.WatchOutboxPath + @"\" + data.FileName, data.FileBody);

                        AppLog.WriteMessage("Успешное создание файла на отправку: " + ConfigWork.WatchOutboxPath + @"\" + data.FileName);
                    }                    
                }
                catch(Exception ex)
                {
                    AppLog.WriteErrorMessage("Ошибка создания фала на отправку. Исключение: " + ex.Message);
                }

                //GetOutgoingEMails();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в методе GetOutcomingMessage. Исключение: " + ex.Message);
                AppLog.WriteErrorMessage("Ошибка в методе GetOutcomingMessage. Исключение: " + ex.Message);
            }
        }
        public static void GetOutgoingEMails()
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient() 
                {
                    Host = "192.168.2.50",
                    Credentials = new NetworkCredential("exchange.mars@maps5.ru", "5667")
                };

                MailMessage msg;
                var results = db.procGetOutgoingEMails();

                foreach(var data in results)
                {
                    msg = new MailMessage();
                    msg.Subject = data.EMailSubj;
                    msg.Body = data.EMailBody;
                    msg.To.Add(data.EMailTo);
                    msg.From = new MailAddress("exchange.mars@maps5.ru");
                    //smtpClient.Send(msg);

                    AppLog.WriteMessage("Успешная отправка EMAIL. Кому: " + data.EMailTo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в методе GetOutgoingEMails. Сообщения не доставлены. Исключение: " + ex.Message);
                AppLog.WriteErrorMessage("Ошибка в методе GetOutgoingEMails. Сообщения не доставлены. Исключение: " + ex.Message);
            }
        }       
    }
}
