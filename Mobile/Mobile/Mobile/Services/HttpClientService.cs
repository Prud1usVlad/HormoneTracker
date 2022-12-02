using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Mobile.Services
{
    public abstract class HttpClientService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _options;

        public HttpClientService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(App.Current.Properties["apiAddress"].ToString()),
            };

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    }
}
