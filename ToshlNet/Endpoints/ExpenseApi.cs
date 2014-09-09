using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ToshlNet.Helpers;
using ToshlNet.Models;

namespace ToshlNet.Endpoints
{
    public class ExpenseApi
    {
        public string AccessToken { get; set; }

        private readonly HttpRequestHandler _requestHandler;

        public ExpenseApi()
            : this(null)
        {

        }

        public ExpenseApi(string accessToken)
        {
            AccessToken = accessToken;

            _requestHandler = new HttpRequestHandler();
        }

        public async Task<Expense[]> List()
        {
            HttpRequestItem httpRequestItem = new HttpRequestItem()
            {
                Url = "https://api.toshl.com/expenses",
                HttpMethod = HttpMethod.Get,
                AuthHeaderValue = new AuthenticationHeaderValue("Bearer", AccessToken)
            };

            HttpResponseMessage httpResponseMessage = await _requestHandler.RequestAsync(httpRequestItem);

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            Expense[] expenses = JsonConvertHelper.DeserializeObject<Expense[]>(content);

            return expenses;
        }

        public async Task<Expense> Get(int expenseId)
        {
            HttpRequestItem httpRequestItem = new HttpRequestItem()
            {
                Url = "https://api.toshl.com/expenses/" + expenseId,
                HttpMethod = HttpMethod.Get,
                AuthHeaderValue = new AuthenticationHeaderValue("Bearer", AccessToken)
            };

            HttpResponseMessage httpResponseMessage = await _requestHandler.RequestAsync(httpRequestItem);

            string content = await httpResponseMessage.Content.ReadAsStringAsync();

            Expense expense = JsonConvertHelper.DeserializeObject<Expense>(content);

            return expense;
        }

        public async Task<Validation<string>> Create(Expense expense)
        {
            IEnumerable<KeyValuePair<string, string>> dict = UrlFormEncoded.Encode(expense);

            HttpRequestItem httpRequestItem = new HttpRequestItem()
            {
                Url = "https://api.toshl.com/expenses/",
                HttpMethod = HttpMethod.Post,
                AuthHeaderValue = new AuthenticationHeaderValue("Bearer", AccessToken),
                HttpContent = new FormUrlEncodedContent(dict)
            };

            HttpResponseMessage httpResponseMessage = await _requestHandler.RequestAsync(httpRequestItem);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string id = httpResponseMessage.Headers.GetValues("Location").First();
                id = id.Split('/').Last();

                return new Validation<string>()
                {
                    ErrorMessages = new string[0],
                    ReturnObject = id
                };
            }

            return new Validation<string>()
            {
                ErrorMessages = new string[]
                {
                    httpResponseMessage.ReasonPhrase
                }
            };
        }
    }
}
