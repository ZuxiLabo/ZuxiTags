namespace ZuxiTags.VRCX
{
    internal class VRCXHandler
    {
        internal static void HandleShowUserDialog(string userId)
        {
            if (string.IsNullOrEmpty(userId)) { return; }
            Program.ServerCon.GetUserTagByID(userId, null, (success, data) =>
             {
               if (success)
                Program.ipcClient.SetCustomTag(userId, data.CustomRank, data.CustomTagColor);
             }
            );
        }
    }
}
