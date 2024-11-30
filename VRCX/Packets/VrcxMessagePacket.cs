using Newtonsoft.Json;
namespace ZuxiTags.VRCX.IPC.Packets
{
    public class VrcxMessagePacket
    {
        public enum MessageType
        {
            VrcxMessage,
            Noty,
            CustomTag,
            External
        }

        [JsonProperty("Data")]
        public string? Data { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; } = "VrcxMessage";

        [JsonProperty("UserId")]
        public string? UserId { get; set; }

        [JsonProperty("DisplayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("Tag")]
        public string? Tag { get; set; }

        [JsonProperty("TagColour")]
        public string? TagColour { get; set; }

        [JsonProperty("MsgType")]
        public string? MsgType { get; set; }

        public VrcxMessagePacket(MessageType messageType)
        {
            MsgType = messageType.ToString();
        }
    }


}
