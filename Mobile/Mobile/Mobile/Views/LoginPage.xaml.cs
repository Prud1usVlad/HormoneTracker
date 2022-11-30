using Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel viewModel { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            viewModel = new LoginViewModel();

            BindingContext = viewModel;
        }
    }
}