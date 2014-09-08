using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToshlNet.Models;

namespace ToshlNet.Helpers
{
    public class StringJoinSerializer : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var items = (string[]) value;

            foreach (var item in items)
            {
                writer.WriteStartObject();
                //writer.
            }

            //serializer.Serialize(writer, data);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<string[]>(reader);
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(string[]))
                return true;
            return false;
        }
    }
}
