using System.Net.Http;
using System.Net.Http.Headers;

namespace ToshlNet.Models
{
    public class HttpRequestItem
    {
        public string Url { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public AuthenticationHeaderValue AuthHeaderValue { get; set; }

        public HttpContent HttpContent { get; set; }
    }
}
