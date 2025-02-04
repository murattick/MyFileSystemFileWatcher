using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyFileSystemFileWatcher
{
    class Program
    {
        private static string ConfigWorkFileName = "";
        private static string ConfigTestFileName = "";
        private static readonly ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public static void Main()
        {
            Console.WindowHeight = 40;
            Console.WindowWidth = 150;

           
            bool x = ReadConfigFiles();

            if (x)
            {
                RunWatchers();
            }
                
        }

        private static bool ReadConfigFiles()
        {
            try
            {
                ConfigWorkFileName = FindConfig.ConfigWorkFilePath();

                bool x = ReadConfig.ReadWorkConfig(ConfigWorkFileName);

                return x;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private static void RunWatchers()
        {
            try
            {
                Task.Factory.StartNew(RunWatcherInbox);
                Task.Factory.StartNew(RunWatcherOutbox);
                bool stop = false;

                //while (true)
                //{
                //    stop = manualResetEvent.WaitOne(20000, true);
                //    if (stop)
                //        return;
                //    Execute.GetOutcomingMessage();
                //}

                //Console.WriteLine("Press 'q' to quit the programm.");
                //Console.WriteLine();
                while (true)
                {
                    stop = manualResetEvent.WaitOne(20000, true);
                    if (stop)
                        return;
                    Execute.GetOutcomingMessage();

                   //while (Console.Read() != 'q') ;
                };
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        private static void RunWatcherOutbox()
        {
            try
            {
                WatcherFiles.RunInbox(true, ConfigWork.WatchInboxPath);
            }
            catch(Exception ex)
            {
                WatcherFiles.RunInbox(false);
                Console.WriteLine(ex.Message);
            }
            
        }

        private static void RunWatcherInbox()
        {
            try
            {
                WatcherFiles.RunOutbox(true, ConfigWork.WatchOutboxPath);
            }
            catch (Exception ex)
            {
                WatcherFiles.RunOutbox(false);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
