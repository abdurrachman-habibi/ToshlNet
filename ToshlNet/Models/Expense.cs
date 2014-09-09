using Newtonsoft.Json;
using ToshlNet.Helpers;

namespace ToshlNet.Models
{
    public class Expense
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "rate")]
        public int? Rate { get; set; }

        [JsonProperty(PropertyName = "date")]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "desc")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "tags")]        
        public string[] Tags { get; set; }        
    }
}
