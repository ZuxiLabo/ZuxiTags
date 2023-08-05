using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZuxiTags
{
    internal class VRCPlayer
    {
        [JsonProperty("id")]
        public string? id { get; set; }

        [JsonProperty("displayName")]
        public string? displayName { get; set; }

        [JsonProperty("bio")]
        public string? bio { get; set; }

        [JsonProperty("bioLinks")]
        public List<string>? bioLinks { get; set; }

        [JsonProperty("currentAvatarImageUrl")]
        public string? currentAvatarImageUrl { get; set; }

        [JsonProperty("currentAvatarThumbnailImageUrl")]
        public string? currentAvatarThumbnailImageUrl { get; set; }

        [JsonProperty("fallbackAvatar")]
        public string? fallbackAvatar { get; set; }

        [JsonProperty("userIcon")]
        public string? userIcon { get; set; }

        [JsonProperty("profilePicOverride")]
        public string? profilePicOverride { get; set; }

        [JsonProperty("statusDescription")]
        public string? statusDescription { get; set; }

        [JsonProperty("status")]
        public string? status { get; set; }

        [JsonProperty("last_platform")]
        public string? last_platform { get; set; }

        [JsonProperty("isFriend")]
        public bool? isFriend { get; set; }

        [JsonProperty("tags")]
        public List<string>? tags { get; set; }

        [JsonProperty("developerType")]
        public string? developerType { get; set; }
    }
}
