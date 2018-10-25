using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Atlas.Efes.Core
{
    public class CryptoHelper
    {
        public static string GetChecksum(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    byte[] checksummer = md5.ComputeHash(stream);
                    return BitConverter.ToString(checksummer).Replace("-", string.Empty);
                }
            }
        }
    }
}
