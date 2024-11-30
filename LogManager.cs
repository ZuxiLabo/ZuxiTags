using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuxiTags
{
    internal class LogManager
    {
        static LogManager()
        {
           
        }
        private static readonly string logFilePath = "latest.txt";

        static bool HasDeleted = false;
        public static void DeleteOldLog()
        {
            try { File.Delete(logFilePath); } catch(Exception) { }
          
            HasDeleted = true;
        }
      //  static StreamWriter writer = new StreamWriter(logFilePath, false);
        public static void Log(string format, params object[] args)
        {
            if (!HasDeleted)
                DeleteOldLog();

          File.AppendAllText(logFilePath, string.Format(format, args) + "\n");
            
          Console.WriteLine(string.Format(format, args));
        }
    }
}
