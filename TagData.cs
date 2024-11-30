

using Newtonsoft.Json;

namespace ZuxiTags
{
    internal class TagData
    {
        [JsonProperty("custom_rank")]
        [JsonConverter(typeof(HtmlStripperConverter))]
        public string CustomRank { get; set; }

        private string _customTagColor;

        [JsonProperty("custom_tag_color")]
        public string CustomTagColor
        {
            get => _customTagColor;
            set => _customTagColor = string.IsNullOrEmpty(value) ? "#FFD1DC" : value; // Set default if null or empty
        }

        [JsonProperty("error")]
        public string error;

        [JsonProperty("username")]
        public string username;
        [JsonProperty("vrchat_id")]
        public string userId;

    }
}
