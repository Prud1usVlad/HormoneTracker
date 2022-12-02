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

namespace Mobile.ViewModels
{
    public class RecomendationsViewModel : BaseViewModel
    {
        private readonly IRecomendationsService _recomendationsService;

        private ObservableCollection<Recomendation> recomendations;
        private ObservableCollection<Tip> tips;
        private int tipsHeight;
        private int recomendationsHeight;

        public ObservableCollection<Tip> Tips
        {
            get { return tips; }
            set
            {
                tips = value;
                OnPropertyChanged("Tips");
            }
        }
        public ObservableCollection<Recomendation> Recomendations
        {
            get { return recomendations; }
            set
            {
                recomendations = value;
                OnPropertyChanged("Recomendations");
            }
        }

        public int TipsHeight
        {
            get { return tipsHeight; }
            set 
            { 
                tipsHeight = value;
                OnPropertyChanged(nameof(TipsHeight));
            }
        }
        public int RecomendationsHeight
        {
            get { return recomendationsHeight;}
            set 
            { 
                recomendationsHeight = value;
                OnPropertyChanged(nameof(RecomendationsHeight));
            }
        }
        public ICommand LoadTips { get; set; }
        public ICommand LoadRecomendations { get; set; }

        public RecomendationsViewModel()
        {
            _recomendationsService = DependencyService.Get<IRecomendationsService>();

            Tips = new ObservableCollection<Tip>();
            Recomendations = new ObservableCollection<Recomendation>();

            LoadTips = new Command(async () => await OnLoadTips());
            LoadRecomendations = new Command(async () => await OnLoadRecomendations());

        }

        public async Task OnLoadTips()
        {
            try
            {
                var tempTips = await _recomendationsService.LoadTips(userId);
                Tips.Clear();
                tempTips.ForEach(m => Tips.Add(m));

                if (Tips.Count == 0)
                    TipsHeight = 50;
                else
                    TipsHeight = -1;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }

        public async Task OnLoadRecomendations()
        {
            try
            {
                var tempRecomendations = await _recomendationsService.LoadRecomendations(userId);
                Tips.Clear();
                tempRecomendations.ForEach(m => Recomendations.Add(m));

                if (Recomendations.Count == 0)
                    RecomendationsHeight = 50;
                else
                    RecomendationsHeight = -1;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }

    }
}
