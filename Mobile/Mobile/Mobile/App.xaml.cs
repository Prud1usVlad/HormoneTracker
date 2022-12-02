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
            Properties["userId"] = 5;

            DependencyService.Register<IAccountService, AccountService>();
            DependencyService.Register<IAnalysisService, AnalysisService>();
            DependencyService.Register<IAidService, AidService>();
            DependencyService.Register<IRecomendationsService, RecomendationsService>();
            DependencyService.Register<IStateService, StateService>();

            MainPage = new LoginPage();

            //MainPage = new AppShell();
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
