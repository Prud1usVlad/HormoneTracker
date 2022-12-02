using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Mobile.Services;
using Mobile.Models;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Mobile.Views;

namespace Mobile.ViewModels
{
    public class NewMedicineViewModel : BaseViewModel
    {
        private readonly IAidService _aidService;
        private Medicine medicine;

        public Medicine Medicine
        {
            get { return medicine; }
            set
            {
                medicine = value;
                OnPropertyChanged(nameof(Medicine));
            }
        }

        public ICommand SaveMedicine { get; set; }
        public ICommand Exit { get; set; }

        public NewMedicineViewModel()
        {
            Medicine = new Medicine();
            _aidService = DependencyService.Get<IAidService>();

            Exit = new Command(async () => await OnExit());
            SaveMedicine = new Command(async () => await OnSaveMedicinde());
        }

        private async Task OnExit()
        {
            await Shell.Current.GoToAsync("..");
        } 

        private async Task OnSaveMedicinde()
        {
            try
            {
                Medicine.LastDoseDate = DateTime.Now;
                Medicine.PatientId = userId;

                await _aidService.AddMedicine(Medicine);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }

    }
}
