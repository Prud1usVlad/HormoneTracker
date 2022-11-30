using Mobile.Services;
using Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Properties["apiAddress"] = "http://192.168.1.239:5070/api/";

            //DependencyService.Register<MockDataStore>();
            DependencyService.Register<AccountService>();
            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
