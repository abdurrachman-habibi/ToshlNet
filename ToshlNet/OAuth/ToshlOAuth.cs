using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ToshlNet.Helpers;
using ToshlNet.Models;

namespace ToshlNet.OAuth
{
    public interface IToshlOAuth
    {
        
    }

    public class ToshlOAuth : IToshlOAuth
    {
        public ToshlClient ToshlClient { get; set; }

        public ToshlOAuth()
        {
            
        }

        public ToshlOAuth(ToshlClient toshlClient)
        {
            ToshlClient = toshlClient;
        }

        public string GetAuthorizeUri()
        {
            string url = "https://toshl.com/oauth2/authorize";

            url += string.Format("?client_id={0}", WebUtility.UrlEncode(ToshlClient.Id));
            url += string.Format("&response_type=code");
            url += string.Format("&state={0}", Guid.NewGuid().ToString().Replace("-", ""));

            return url;
        }

        public async Task<OAuthToken> GetAccessToken(string callbackUrl)
        {
            //TODO hasn't handled error issue

            Uri uri = new Uri(callbackUrl);

            HttpValueCollection queryString = HttpUtility.ParseQueryString(uri.Query);

            string callbackCode = queryString["code"];

            Dictionary<string,string> contentDict = new Dictionary<string, string>()
            {
                {"code", callbackCode},
                {"grant_type", "authorization_code"}
            };

            string requestContent = JsonConvertHelper.SerializeObject(contentDict);

            OAuthToken oAuthToken = await RequestToken(contentDict);

            return oAuthToken;
        }

        public async Task<OAuthToken> RefreshToken(string refreshToken)
        {
            Dictionary<string, string> contentDict = new Dictionary<string, string>()
            {                
                {"grant_type", "refresh_token"},
                {"refresh_token", refreshToken}
            };

            string requestContent = JsonConvertHelper.SerializeObject(contentDict);

            OAuthToken oAuthToken = await RequestToken(contentDict);

            return oAuthToken;
        }

        private async Task<OAuthToken> RequestToken(Dictionary<string, string> requestContent)
        {

            HttpClientHandler handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.GetEncoding("ISO-8859-1").GetBytes(
                            string.Format("{0}:{1}", ToshlClient.Id, ToshlClient.Secret))));

            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri("https://toshl.com/oauth2/token"),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(requestContent)
            };

            HttpResponseMessage httpResponseMessage = await client.SendAsync(httpRequest);

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            Dictionary<string, dynamic> dict = JsonConvertHelper.DeserializeObject<Dictionary<string, dynamic>>(content);

            OAuthToken oAuthToken = new OAuthToken()
            {
                AccessToken = dict["access_token"],
                TokenType = dict["token_type"],
                ExpiresIn = dict["expires_in"],
                RefreshToken = dict["refresh_token"],
                Scope = dict["scope"]
            };

            return oAuthToken;
        }
    }
}
