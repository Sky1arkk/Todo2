using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Todo2.WinPhone
{
    class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            // Use the SpecialFolder enum to get the Personal folder on the Android file system.
            // Storing the database here is a best practice.
            string documentsPath = ApplicationData.Current.LocalFolder.Path;
            return System.IO.Path.Combine(documentsPath, filename);
        }
    }
}
