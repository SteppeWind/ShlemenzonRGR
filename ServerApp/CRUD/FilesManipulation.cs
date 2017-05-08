using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.CRUD
{
    public static class FilesManipulation
    {
        public static void CreateFile(byte[] array, string path)
        {
            if (array.Length != 0)
            {
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    fs.Write(array, 0, array.Length);
                }
            }
        }

        public static void DeleteFile(string path) => File.Delete(path);

    }
}