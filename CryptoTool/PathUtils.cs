using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    }
}
