using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CryptoTool
{
    public static class PathUtils
    {
        public static string GetFullPath(string path)
        {
            try
            {
                string s = Path.GetFullPath(path);
                return s.TrimEnd('\\');
            }
            catch
            {
                return null;
            }
        }

        public static string GetSettingsFilePath()
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CryptoTool");
            var file = Path.Combine(dir, "settings.ini");

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return file;
        }
    }
}
