using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace Mobile.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;

        public AccountService()
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(App.Current.Properties["apiAddress"].ToString()),
            };

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<bool> Login(string email, string password)
        {
            var response = await _httpClient.PostAsync("Security/Tokens",
                new StringContent(JsonSerializer.Serialize(new { Email = email, Password = password }),
                    Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var values = JsonSerializer.Deserialize<Dictionary<string, string>>(
                    await response.Content.ReadAsStringAsync());

                App.Current.Properties["token"] = values["Token"];
                App.Current.Properties["userId"] = values["UserId"];

                return true;
            }

            return false;
        }

        public async Task Logout()
        {
            App.Current.Properties["token"] = null;
            await AppShell.Current.GoToAsync($"//{nameof(Views.LoginPage)}");
        }
    }
}
