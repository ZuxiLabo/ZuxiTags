using System;
using System.Text.RegularExpressions;

namespace ZuxiTags
{

    internal class Program
    {

       public static Server _Server = new Server();
        public  static LogFileMonitor _WatchCleaner = null;
        public  static string LocalUser = string.Empty;

        public static void Main(string[] args)
        {

            ResourceUtils.ExtractResources();


            Console.WriteLine("EAC is a thing doesnt mean we cant have fun... you may request a tag in the discord => uwu.ac/discord");



            FileUtils.FindVRChatLatestFile();


            GetLocalUserData();


            _WatchCleaner = new LogFileMonitor(FileUtils.LogFile, "\r\n") { };
            _WatchCleaner.OnLine += (s, e) =>
            {
                OnNewLine(e.Line);
            };
            _WatchCleaner.Start();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.Title = "ZuxiTags by Zuxi ";

            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            Console.WriteLine("Found Local user: " + LocalUser);

            Console.Title = Console.Title + " | UserName: " + LocalUser;

            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            Console.WriteLine("Connected To VRChat Log File Successully");


            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            Console.WriteLine("Fetching Local User Data...");


            VRCPlayer _Player = PlayerManager.GetUserFromJSON(_Server.getUserByName(LocalUser), LocalUser);

            Console.ForegroundColor = ConsoleColor.Cyan;//ConsoleUtils.GetNewConsoleColor(); 
            Console.WriteLine("Got Local User Successfully; \nUserName => {1} \nID => {0} \nTag=> {2} ", _Player.id, _Player.displayName, _Server.GetUserTagByID(_Player.id));

            Console.WriteLine();

            Console.Title = Console.Title + " | UserID: " + _Player.id;





            Console.Read();
        }


        internal static void OnNewLine(string line)
        {
            if (line.Contains("OnPlayerJoined"))
            {
                string PlayerName = line.Substring(61).Trim();


                PlayerManager.OnPlayerJoined(PlayerName);
            }

            if (line.Contains("OnPlayerLeft"))
            {
                string PlayerName = line.Substring(58).Trim();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Player Leave: {PlayerName}");
                Console.ForegroundColor = ConsoleColor.Cyan;
            }


        }

        internal static void GetLocalUserData()
        {

            string LogFileString = FileUtils.GetallTextFromLogFile();



            if (LogFileString.Contains("Authenticated:"))
            {
                //  string pattern = @"(?<=\bLog\s+-\s+\[Behaviour\] User Authenticated: ).{0,64}(?=\r\n|$)";
                string pattern = @"Authenticated:\s+(.*?)\s+\(";
                Match match = Regex.Match(LogFileString, pattern);

                if (match.Success)
                {
                    string logMessage = match.Groups[1].Value;

                    LocalUser = logMessage;
                    // Console.WriteLine("Log Message: " + logMessage);
                }
                else
                {
                    Console.WriteLine("No match found.");
                }




            }
        }



    }
}
