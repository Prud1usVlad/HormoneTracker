using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class RecomendationsService : HttpClientService, IRecomendationsService
    {
        public async Task<List<Recomendation>> LoadRecomendations(int userId)
        {
            var responce = await _httpClient.GetAsync($"Recomendations/Patient/{userId}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to load recomendations", "Ok");
                return new List<Recomendation>();
            }
            else
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Recomendation>>(jsonString, _options);
            }
        }

        public async Task<List<Tip>> LoadTips(int userId)
        {
            var responce = await _httpClient.GetAsync($"Tips/ByUserId/{userId}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to load tips", "Ok");
                return new List<Tip>();
            }
            else
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Tip>>(jsonString, _options);
            }
        }
    }
}
