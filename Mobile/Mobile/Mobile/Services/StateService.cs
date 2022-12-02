using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class StateService : HttpClientService, IStateService
    {
        public async Task<List<Analysis>> LoadAnalysis(int userId)
        {
            var responce = await _httpClient.GetAsync($"Analyses/ByUserId/{userId}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to load analyses", "Ok");
                return new List<Analysis>();
            }
            else
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Analysis>>(jsonString, _options);
            }
        }

        public async Task<ChartData> LoadAnalysisChartData(int userId, string name)
        {
            var responce = await _httpClient.GetAsync($"Charts/Patient/{userId}/{name}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to load chart data", "Ok");
                return new ChartData();
            }
            else
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ChartData>(jsonString, _options);
            }
        }
    }
}
