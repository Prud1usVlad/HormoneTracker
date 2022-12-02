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
    public class AidViewModel : BaseViewModel
    {
        private readonly IAidService _aidService;

        private Doctor doctor;
        private ObservableCollection<Medicine> medicines;
        
        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        public ObservableCollection<Medicine> Medicines
        {
            get { return medicines; }
            set
            {
                medicines = value;
                OnPropertyChanged("Medicines");
            }
        }

        public ICommand CallDoctor { get; set; }
        public ICommand LoadDoctor { get; set; }
        public ICommand LoadMedicines { get; set; }
        public ICommand AddMedicine { get; set; }
        public ICommand DeleteMedicine { get; set; }
        public ICommand ItemTapped { get; set; }

        public AidViewModel()
        {
            _aidService = DependencyService.Get<IAidService>();

            CallDoctor = new Command(() => OnCallDoctor());
            LoadDoctor = new Command(async () => await OnLoadDoctor());
            LoadMedicines = new Command(async () => await OnLoadMedicines());
            DeleteMedicine = new Command(async (id) => await OnDeleteMedicine(int.Parse(id.ToString())));
            AddMedicine = new Command(async () => await OnAddMedicine());
            ItemTapped = new Command(async (medicine) => await OnUpdateMedicine(medicine));

            Medicines = new ObservableCollection<Medicine>() { new Medicine() };
            Doctor = new Doctor();

        }

        private void OnCallDoctor()
        {
            try
            {
                _aidService.CallDoctor(Doctor);
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }

        private async Task OnLoadDoctor()
        {
            try
            {
                var newDoctor = await _aidService.LoadDoctorInfo(userId);
                Doctor = newDoctor;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }
        
        private async Task OnLoadMedicines()
        {
            try
            {
                var tempMedicines = await _aidService.LoadUserMedicines(userId);
                Medicines.Clear();
                tempMedicines.ForEach(m => Medicines.Add(m));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }
        
        private async Task OnDeleteMedicine(int id)
        {
            try
            {
                await _aidService.DeleteMedicine(id);
                await OnLoadMedicines();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }

        private async Task OnAddMedicine()
        {
            await Shell.Current.GoToAsync(nameof(NewMedicinePage));
        }

        private async Task OnUpdateMedicine(object medicine)
        {
            try
            {
                var currentMedicine = medicine as Medicine;
                bool answer = await App.Current.MainPage.DisplayAlert("Have you taken pills?",
                    $"Have you taken medicine: {currentMedicine.Name}?", "Yes", "No");

                if (currentMedicine != null && answer)
                {
                    currentMedicine.AmountLast--;
                    
                    if (currentMedicine.AmountLast == 0)
                        await OnDeleteMedicine(currentMedicine.MedicineId);
                    else
                    {
                        currentMedicine.LastDoseDate = DateTime.Now;
                        await _aidService.UpdateMedicine(currentMedicine);
                        await OnLoadMedicines();
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }
    }
}
