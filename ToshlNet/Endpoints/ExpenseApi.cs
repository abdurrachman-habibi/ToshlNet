using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public async Task<int> Create(Expense expense)
        {
            HttpRequestItem httpRequestItem = new HttpRequestItem()
            {
                Url = "https://api.toshl.com/expenses/",
                HttpMethod = HttpMethod.Post,
                AuthHeaderValue = new AuthenticationHeaderValue("Bearer", AccessToken),
                HttpContent = new FormUrlEncodedContent(JsonConvertHelper.SerializeToDictionary(expense))
            };

            HttpResponseMessage httpResponseMessage = await _requestHandler.RequestAsync(httpRequestItem);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return 123;
            }
            else
            {
                return 0;
            }
        }
    }
}
