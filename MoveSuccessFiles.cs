using System;
using System.IO;
using System.Threading;

namespace MyFileSystemFileWatcher
{
    public class MoveSuccessFiles
    {
        public static void MoveFile(string sourceFile, string destinationFile, string folder, string fileName)
        {
            try
            {                
                File.Move(sourceFile, destinationFile + @"\" + folder + @"\" + fileName);
                //Thread.Sleep(700);
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("Файл: {0} небыл перемещен по причине: {1}", sourceFile, ex.Message));
                AppLog.WriteErrorMessage(string.Format("Файл: {0} небыл перемещен по причине: {1}", sourceFile, ex.Message));
            }
        }
    }
}
