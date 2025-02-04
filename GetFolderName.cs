using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFileSystemFileWatcher
{
    public static class GetFolderName
    {
        public static string GetName(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(path))
                    return null;

                string folder = null;
                string inbox = "inbox";
                string outbox = "outbox";

                switch (path)
                {
                    case var data when data.IndexOf(inbox) > 0:
                        folder = inbox;
                        break;
                    case var data when data.IndexOf(outbox) > 0:
                        folder = outbox;
                        break;
                }

                return folder;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при определении папки (inbox || outbox) " + ex.Message);
                AppLog.WriteErrorMessage("Ошибка при определении папки (inbox || outbox) " + ex.Message);
                return null;
            }
        }
    }
}
