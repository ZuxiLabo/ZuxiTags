using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZuxiTags
{

    public class HtmlStripperConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Read the string value
            var rawValue = (string)reader.Value;

            // Remove HTML tags
            return StripHtml(rawValue);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Write the string value as is
            writer.WriteValue(value);
        }

        private string StripHtml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Use regex to remove HTML tags
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
    }
}
