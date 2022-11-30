using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public AnalysisService()
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

        public async Task AddAnalysis(Analysis analysis)
        {
            var responce = await _httpClient.PostAsync("Analysis",
                new StringContent(JsonSerializer.Serialize(analysis),
                    Encoding.UTF8, "application/json"));

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to sent data", "Ok");
            }
        }

        public Task<List<Analysis>> GetAnalysis(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChartData>> GetAnalysisChartData(int userId, string name)
        {
            var responce = await _httpClient.GetAsync($"ChartData/{userId}/{name}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to sent data", "Ok");
                return new List<ChartData>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<ChartData>>(await responce.Content.ReadAsStringAsync(), _options);
            }
        }

        public Task<Analysis> GetLastLocalAnalysis(int userId)
        {
            return Task.FromResult(new Analysis
            {
                Name = "HemoglobinTest",
                Date = DateTime.Now,
                PatientId = userId,
                Data = new List<Datum>
                {
                    new Datum 
                    { 
                        Name = "Hemoglobin",
                        Value = 12,
                        NormCoefficient = 1.2,
                    }
                }
            });
        }

        public Task RemoveAnalysis(Analysis analysis)
        {
            throw new NotImplementedException();
        }
    }
}
