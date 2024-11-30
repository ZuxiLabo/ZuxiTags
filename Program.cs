using System;
using System.Net;
using System.Text.RegularExpressions;
using ZuxiTags.VRCX.IPC;
namespace ZuxiTags
{

    internal class Program
    {

        internal static Server ServerCon = new Server();
        internal static LogFileMonitor LogWatcher = null;
        internal static string LocalUserName = string.Empty;
        internal static string LocalUserId = string.Empty;
        internal static string LocalUser = string.Empty;
        internal static readonly IPCClient ipcClient = new IPCClient();
        internal static readonly IPCClientReceive ipcClientRec = new IPCClientReceive();
        [Zuxi.SDK.DoNotObfuscate]
        internal static void Main(string[] args)
        {
            ResourceUtils.RegisterAssemResolver();
          //  ResourceUtils.ExtractResources();
            LogManager.Log(new WebClient().DownloadString("https://zuxi.dev/projects/vrctags/startupmessage.txt"));

            ipcClient.Connect();
            ipcClientRec.Connect();
            // Start the receiver server
            #region VRChatLogStuff
           
            FileUtils.FindVRChatLatestFile();


            GetLocalUserData();


            LogWatcher = new LogFileMonitor(FileUtils.LogFile, "\r\n") { };
            LogWatcher.OnLine += (s, e) =>
            {
                OnNewLine(e.Line);
            };
            LogWatcher.Start();
            Console.ForegroundColor = ConsoleColor.Cyan;
            
            Console.Title = "ZuxiTags by Zuxi ";

            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            LogManager.Log("Found Local user: " + LocalUser);

            Console.Title = Console.Title + " | UserName: " + LocalUser;

            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            LogManager.Log("Connected To VRChat Log File Successully");


            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            LogManager.Log("Fetching Local User Data...");


            // VRCPlayer _Player = PlayerManager.GetUserFromJSON(_Server.getUserByName(LocalUser), LocalUser);
            ServerCon.GetUserTagByID(LocalUserId, LocalUser, OnLocalUserComplete);
           
            Console.Title = Console.Title + " | UserID: " + LocalUserId;

            #endregion
            Console.Read();
        }

        internal static void OnLocalUserComplete(bool success, TagData data)
        {
            if (success)
            {
                Console.ForegroundColor = ConsoleExt.GetNearestConsoleColor(data.CustomTagColor);//ConsoleUtils.GetNewConsoleColor(); 
                LogManager.Log("Got Local User Successfully; \nUserName => {1} \nID => {0} \nTag=> {2} ", LocalUserId, LocalUserName, data.CustomRank);
            }
            else
            {
                LogManager.Log("Got Local User Successfully; \nUserName => {1} \nID => {0} \nTag=> {2} ", LocalUserId, LocalUserName, "No Tag Set!");
            }
            Console.WriteLine();
        }


        internal static void OnNewLine(string line)
        {
            if (line.Contains("OnPlayerJoined"))
            {
                string PlayerName = line.Substring(61).Trim();
                string UserId = GetUserIDFromJoinLog(line);
                Program.ServerCon.GetUserTagByID(UserId, PlayerName.Replace($"({UserId})", "").Trim(), PlayerManager.OnPlayerLookupComplete);
                
            }

            if (line.Contains("OnPlayerLeft"))
            {
                string PlayerName = line.Substring(58).Trim();
                Console.ForegroundColor = ConsoleColor.Red;
                LogManager.Log($"Player Leave: {PlayerName}");
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
                    LocalUserName = logMessage;
                    LocalUserId = GetUserIDFromJoinLog(LogFileString);

                    LocalUser = logMessage;
                    // LogManager.Log("Log Message: " + logMessage);
                }
                else
                {
                    LogManager.Log("No match found.");
                }
            }
        }

        internal static string GetUserIDFromJoinLog(string joinlog)
        {
            string pattern = @"usr_[a-f0-9-]+";
            Match match = Regex.Match(joinlog, pattern);

            if (match.Success)
            {
                string userId = match.Value;
                // LogManager.Log($"Extracted user ID: {userId}");

                return userId;
            }
            return "";
        }

    }
}
