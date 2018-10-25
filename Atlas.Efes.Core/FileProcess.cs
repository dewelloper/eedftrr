using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Core
{
    public class FileProcess
    {
        public static byte[] CreateZipFile(string sourceFileName, string destFileName)
        {
            using (var zip = new Ionic.Zip.ZipFile())
            {

                zip.AddFile(sourceFileName, string.Empty);
                zip.Save(destFileName);
            }

            byte[] zipfile = File.ReadAllBytes(destFileName);
            return zipfile;
        }


        public static void SaveFile(string path)
        {

        }
    }
}
