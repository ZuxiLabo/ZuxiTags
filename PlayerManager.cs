using Newtonsoft.Json.Linq;
using System;

namespace ZuxiTags
{
    internal class PlayerManager
    {
        public static void OnPlayerJoined(string anem)
        {
            VRCPlayer player = Program._Server.GetVRCPlayerbyName(anem);

            if (player is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to Fetch User Info for user they probably have special chars in there name :P PlayerJoined: " + anem);
                Console.WriteLine("[OnFailedRequest] => Falling Back to attempt to get user info => " + Program._Server.GetVRCPlayerbyName(anem));


                return;
            }


            string Tag = Program._Server.GetUserTagByID(player.id);

            Console.ForegroundColor = ConsoleUtils.GetNewConsoleColor();
            Console.WriteLine("Player Join: {0} id: {1} tag: {2}", anem, player.id, Tag);
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
