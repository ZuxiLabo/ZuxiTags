using System;
using System.IO;
using System.Linq;

namespace ZuxiTags
{
    internal class FileUtils
    {
        internal static string _VRCDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "Low\\VRChat\\VRChat";
        internal static string LogFile = string.Empty;
     

        public static string GetallTextFromLogFile()
        {

            // Open the file with the FileShare.ReadWrite option to allow reading while the file is open by another process
            using (FileStream fileStream = new FileStream(LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(fileStream))
            {
                // Read all text from the file
                string fileText = reader.ReadToEnd();
                return fileText;
            }

        }


        public static void FindVRChatLatestFile()
        {
            try
            {
                // Get all files with ".txt" extension in the specified directory
                var txtFiles = Directory.GetFiles(_VRCDirectory, "*.txt");

                if (txtFiles.Length > 0)
                {
                    // Sort files by creation time (descending) and select the first (most recent) file
                    string latestTxtFile = txtFiles.OrderByDescending(f => File.GetCreationTime(f)).First();
                    Console.WriteLine("Latest .txt file: " + latestTxtFile);

                    LogFile = latestTxtFile;
                }
                else
                {
                    Console.WriteLine("No .txt files found in the directory.");
                }
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Directory not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }



    }
}
