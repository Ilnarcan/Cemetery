using System;
using System.IO;
using Application = System.Windows.Forms.Application;

namespace Cemetery
{
    internal class BaseFunctions
    {
        private static string _basefile = @"system.config";
        private static string _inputfile = @"system.update";

        public static string Getbase()
        {
            return _basefile;
        }

        public static void Setbase(string value)
        {
            _basefile = value;
        }

        public static string Getinput()
        {
            return _inputfile;
        }

        public static void Setinput(string value)
        {
            _inputfile = value;
        }

        public static void SaveDataBase()
        {
            string databaseNamecrypt = Getbase();

            string[] mem = databaseNamecrypt.Split('.');

            DateTime curDate = DateTime.Now.Date;
            string currentTimeStr = curDate.Date.ToString("ddMMyyyy");

            string databaseNamecryptnew = mem[0] + " " + currentTimeStr + "." + mem[1];


            string dir = Application.StartupPath;
            string direct = dir + "\\Archive";

            if (!Directory.Exists(direct))
                Directory.CreateDirectory(direct);

            string path2 = dir + "\\Archive\\" + databaseNamecryptnew;

            if (File.Exists(path2))
                File.Delete(path2);

            File.Copy(databaseNamecrypt, dir + "\\Archive\\" + databaseNamecrypt);

            File.Move(dir + "\\Archive\\" + databaseNamecrypt, path2);

            const string cdirect = "C:\\Archive";

            if (!Directory.Exists(cdirect))
                Directory.CreateDirectory(cdirect);

            string cpath2 = cdirect + "\\" + databaseNamecryptnew;

            if (File.Exists(cpath2))
                File.Delete(cpath2);

            File.Copy(path2, cpath2);

        }

    }
}