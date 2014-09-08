using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToshlNet.Helpers
{
    public static class JsonConvertHelper
    {
        public static T DeserializeObject<T>(string value)
        {
            JsonSerializer jsonSerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            using (var reader = new JsonTextReader(new StringReader(value)))
            {
                return jsonSerializer.Deserialize<T>(reader);
            }
        }

        public static string SerializeObject(object value)
        {
            JsonSerializer jsonSerializer = new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            StringBuilder sb = new StringBuilder(256);
            StringWriter sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
            {
                jsonWriter.Formatting = jsonSerializer.Formatting;

                jsonSerializer.Serialize(jsonWriter, value);
            }

            return sw.ToString();
        }

        public static Dictionary<string, string> SerializeToDictionary(object value)
        {
            string serializeObject = SerializeObject(value);
            Dictionary<string, string> dictionary = DeserializeObject<Dictionary<string, string>>(serializeObject);

            return dictionary;
        } 
    }
}
