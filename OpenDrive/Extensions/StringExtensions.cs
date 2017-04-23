using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class StringExtensions
    {

        public static string CalculateFileMd5(this string path)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(path))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "‌​").ToLower();
                }
            }
        }

    }
}
