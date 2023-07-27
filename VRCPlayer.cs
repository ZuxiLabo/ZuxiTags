using System.Collections.Generic;

namespace ZuxiTags
{
    internal class VRCPlayer
    {

        public string id { get; set; }
        public string displayName { get; set; }
        public string bio { get; set; }
        public List<string> bioLinks { get; set; }
        public string currentAvatarImageUrl { get; set; }
        public string currentAvatarThumbnailImageUrl { get; set; }
        public string fallbackAvatar { get; set; }
        public string userIcon { get; set; }
        public string profilePicOverride { get; set; }
        public string statusDescription { get; set; }
        public string status { get; set; }
        public string last_platform { get; set; }
        public bool isFriend { get; set; }
        public List<string> tags { get; set; }
        public string developerType { get; set; }
    }
}
