using Mobile.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Mobile.Services;

namespace Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _email;
        private string _password;
        private readonly IAccountService _accountService; 

        public string Email
        {
            get { return _email; }
            set 
            { 
                _email = value; 
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public Command LoginCommand { get; }

        public LoginViewModel()
        {
            _accountService = DependencyService.Get<IAccountService>();
            LoginCommand = new Command(OnLoginClicked);
        }

        private async void OnLoginClicked(object obj)
        {

            if (await _accountService.Login(Email, Password))
            {
                // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
                //await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                App.Current.MainPage = new AppShell();
            }
            else
            {
                Password = "";
                await App.Current.MainPage.DisplayAlert("Login failed", "An error accured while logging", "Ok");
            }
        }
    }
}
