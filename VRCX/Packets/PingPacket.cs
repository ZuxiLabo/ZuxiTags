using Newtonsoft.Json;

namespace ZuxiTags.VRCX.IPC.Packets
{
    public class PingPacket
    {
        [JsonProperty("version")]
        public string? Version { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; } = "MsgPing";

        public PingPacket()
        {
        }
    }
}
