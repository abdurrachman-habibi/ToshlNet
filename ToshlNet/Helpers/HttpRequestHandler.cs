﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ToshlNet.Models;

namespace ToshlNet.Helpers
{
    public class HttpRequestHandler
    {
        private readonly HttpClient _client;
        private readonly HttpClientHandler _handler;

        public HttpRequestHandler()
        {
            _handler = new HttpClientHandler();
            _client = new HttpClient(_handler);
        }

        public async Task<HttpResponseMessage> RequestAsync(HttpRequestItem httpRequestItem)
        {
            _client.DefaultRequestHeaders.Authorization = httpRequestItem.AuthHeaderValue;

            HttpRequestMessage httpRequest = new HttpRequestMessage()
            {
                RequestUri = new Uri(httpRequestItem.Url),
                Method = httpRequestItem.HttpMethod,
                Content = httpRequestItem.HttpContent
            };

            if (httpRequestItem.Headers != null)
            {
                foreach (var header in httpRequestItem.Headers)
                {
                    httpRequest.Headers.Add(header.Item1, header.Item2);
                }
            }

            if (httpRequestItem.IfUnmodifiedSinceHeader != DateTime.MinValue)
            {
                httpRequest.Headers.IfUnmodifiedSince = httpRequestItem.IfUnmodifiedSinceHeader;
            }

            HttpResponseMessage httpResponseMessage = await _client.SendAsync(httpRequest);

            return httpResponseMessage;
        }
    }
}
