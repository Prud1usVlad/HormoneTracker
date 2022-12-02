using Mobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Mobile.Services
{
    public class AidService : HttpClientService, IAidService
    {
        public AidService() : base() { }

        public async Task AddMedicine(Medicine medicine)
        {
            var responce = await _httpClient.PostAsync("Medicines", 
                new StringContent(JsonSerializer.Serialize(medicine),
                    Encoding.UTF8, "application/json"));

            if (responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Success", "New medicine added", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured whil trying to save medicine", "Ok");
            }
        }

        public void CallDoctor(Doctor doctor)
        {
            var phoneNumber = "+38" + doctor.Phone;
            try
            {
                PhoneDialer.Open(phoneNumber);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured while trying to make call. " + ex.Message, "Ok");
            }
        }

        public async Task DeleteMedicine(int id)
        {
            var responce = await _httpClient.DeleteAsync("Medicines/" + id);

            if (responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Medicine deleted", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured whil trying to delete medicine", "Ok");
            }
        }

        public async Task<Doctor> LoadDoctorInfo(int userId)
        {
            var responce = await _httpClient.GetAsync($"Doctors/ByUserId/{userId}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to load medicines", "Ok");
                return new Doctor();
            }
            else
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Doctor>(jsonString, _options);
            }
        }

        public async Task<List<Medicine>> LoadUserMedicines(int userId)
        {
            var responce = await _httpClient.GetAsync($"Medicines/ByUserId/{userId}");

            if (!responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Error occured while trying to load medicines", "Ok");
                return new List<Medicine>();
            }
            else
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Medicine>>(jsonString, _options);
            }
        }

        public async Task UpdateMedicine(Medicine medicine)
        {
            var responce = await _httpClient.PutAsync("Medicines/" + medicine.MedicineId,
                new StringContent(JsonSerializer.Serialize(medicine),
                    Encoding.UTF8, "application/json"));

            if (responce.IsSuccessStatusCode)
            {
                await App.Current.MainPage.DisplayAlert("Success", "Medicine data updated", "Ok");
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured whil trying to update medicine", "Ok");
            }
        }
    }
}
