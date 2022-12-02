using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AidPage : ContentPage
    {
        public AidViewModel viewModel;

        public AidPage()
        {
            InitializeComponent();

            viewModel = new AidViewModel();
            BindingContext = viewModel;
            CV.ItemsSource = viewModel.Medicines;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadDoctor.Execute(null);
            viewModel.LoadMedicines.Execute(null);

        }
    }
}