using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToshlNet.Models;

namespace ToshlNet.Helpers
{
    public static class UrlFormEncoded
    {
        public static IEnumerable<KeyValuePair<string, string>> Encode(object o)
        {
            List<KeyValuePair<string, string>> dict = new List<KeyValuePair<string, string>>();

            var properties = o.GetType().GetRuntimeProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<JsonPropertyAttribute>();

                if (property.PropertyType.IsArray)
                {
                    string key = attribute.PropertyName + "[]";

                    var items = (Array)(property.GetValue(o));

                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            if (item != null)
                            {
                                dict.Add(new KeyValuePair<string, string>(key, item.ToString()));
                            }
                        }
                    }
                }
                else
                {
                    string key = attribute.PropertyName;
                    var value = property.GetValue(o);

                    if (value != null)
                    {
                        dict.Add(new KeyValuePair<string, string>(key, value.ToString()));
                    }
                }
            }

            return dict;
        }
    }
}
