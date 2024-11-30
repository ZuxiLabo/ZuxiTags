using Newtonsoft.Json.Linq;
using System;

namespace ZuxiTags
{
    internal class PlayerManager
    {
        public static void OnPlayerJoined(string VRChatId, string anem)
        {
            
        }
        internal static void OnPlayerLookupComplete(bool success, TagData data)
        {
            Console.ForegroundColor = ConsoleExt.GetNearestConsoleColor(data.CustomTagColor);//ConsoleUtils.GetNewConsoleColor();
           
            LogManager.Log("Player: {0} ({1}) tag: {2}", data.username, data.userId, data.CustomRank);
            Console.ForegroundColor = ConsoleColor.Cyan;
        }

        public static VRCPlayer GetUserFromJSON(string json, string strongname)
        {
            JArray JsonArray = JArray.Parse(json);


            for (int k = 0; k < JsonArray.Count; k++)
            {

                string displayName = ((string?)JsonArray[k]["displayName"])?.Trim();


                if (displayName == strongname)
                {
                    return JsonArray[k].ToObject<VRCPlayer>();
                }


            }

            return null;

        }


    }
}
