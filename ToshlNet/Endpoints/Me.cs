using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToshlNet.Helpers;
using ToshlNet.Models;

namespace ToshlNet.Endpoints
{
    public class Me
    {
        public string AccessToken { get; set; }

        private readonly HttpRequestHandler _requestHandler;

        private const string Url = "https://api.toshl.com/me";

        public Me()
            : this(null)
        {

        }

        public Me(string accessToken)
        {
            AccessToken = accessToken;

            _requestHandler = new HttpRequestHandler();
        }

        public async Task<User> Get()
        {
            HttpRequestItem httpRequestItem = new HttpRequestItem()
            {
                Url = Url,
                HttpMethod = HttpMethod.Get,
                AuthHeaderValue = new AuthenticationHeaderValue("Bearer", AccessToken)
            };

            HttpResponseMessage httpResponseMessage = await _requestHandler.RequestAsync(httpRequestItem);

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            User user = JsonConvertHelper.DeserializeObject<User>(content);

            return user;
        }

        public async Task<User> Put(User user)
        {
            HttpRequestItem httpRequestItem = new HttpRequestItem()
            {
                Url = Url,
                HttpMethod = HttpMethod.Put,
                AuthHeaderValue = new AuthenticationHeaderValue("Bearer", AccessToken),
                HttpContent = new FormUrlEncodedContent(JsonConvertHelper.SerializeToDictionary(user)),
                IfUnmodifiedSinceHeader = new DateTime(2014, 08, 02)
            };

            HttpResponseMessage httpResponseMessage = await _requestHandler.RequestAsync(httpRequestItem);

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            User updatedUser = JsonConvertHelper.DeserializeObject<User>(content);

            return updatedUser;
        }
    }
}
