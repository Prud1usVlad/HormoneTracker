using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mobile.ViewModels;

namespace Mobile.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutViewModel ViewModel { get; set; }

        public AboutPage()
        {
            InitializeComponent();
            ViewModel = new AboutViewModel();
            BindingContext = ViewModel;
        }
    }
}