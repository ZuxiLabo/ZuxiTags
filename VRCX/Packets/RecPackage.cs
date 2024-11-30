using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZuxiTags.VRCX.IPC.Packets
{
    public class RecPackage
    {
        [JsonProperty("Data")]
        public string? Data { get; set; }

        [JsonProperty("Type")]
        public string? Type { get; set; }

        [JsonProperty("MsgType")]
        public string? MsgType { get; set; }

        public RecPackage()
        {
        }
    }

}
