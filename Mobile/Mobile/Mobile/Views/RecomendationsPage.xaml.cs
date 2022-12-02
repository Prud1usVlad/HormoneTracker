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
    public partial class RecomendationsPage : ContentPage
    {
        public RecomendationsViewModel viewModel;
        public RecomendationsPage()
        {
            InitializeComponent();

            viewModel = new RecomendationsViewModel();
            BindingContext = viewModel;
            TipsView.ItemsSource = viewModel.Tips;
            RecomendationsView.ItemsSource = viewModel.Recomendations;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            viewModel.LoadRecomendations.Execute(null);
            viewModel.LoadTips.Execute(null);

            //if (viewModel.Recomendations.Count == 0)
            //    RecomendationsView.HeightRequest = 50;
            //else
            //    RecomendationsView.HeightRequest = -1;

            //if (viewModel.Tips.Count == 0)
            //    TipsView.HeightRequest = 50;
            //else 
            //    TipsView.HeightRequest = -1;
        }
    }
}