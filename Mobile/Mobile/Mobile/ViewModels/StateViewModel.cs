using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Mobile.Services;
using Mobile.Models;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Mobile.Views;
using Microcharts.Forms;
using Microcharts.Abstracts;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

namespace Mobile.ViewModels
{
    public class StateViewModel : BaseViewModel
    {
        private readonly IStateService _stateService;

        private ObservableCollection<Analysis> analyses;
        private ObservableCollection<ChartEntry> entries;
        private BarChart chart;

        public ObservableCollection<Analysis> Analyses
        {
            get { return analyses; }
            set
            {
                analyses = value;
                OnPropertyChanged(nameof(Analyses));
            }
        }
        public ObservableCollection<ChartEntry> Entries
        {
            get { return entries; }
            set
            {
                entries = value;
                OnPropertyChanged(nameof(Entries));
            }
        }
        public BarChart Chart
        {
            get { return chart; }
            set
            {
                chart = value;
                OnPropertyChanged(nameof(Chart));
            }
        }

        public ICommand LoadAnalyses { get; set; }
        public ICommand LoadChartData { get; set; }

        public StateViewModel()
        {
            _stateService = DependencyService.Get<IStateService>();

            Analyses = new ObservableCollection<Analysis>();
            Entries = new ObservableCollection<ChartEntry>();

            Entries.Add(new ChartEntry(100) { Label = "empty" });
            Entries.Add(new ChartEntry(100) { Label = "empty" });

            LoadChartData = new Command(async () => await OnLoadChartData());
            LoadAnalyses = new Command(async () => await OnLoadAnalyses());

            Chart = new BarChart() 
            { 
                Entries = Entries,
                LabelTextSize = 26,
                LabelOrientation = Orientation.Horizontal,
            };
        }

        private async Task OnLoadAnalyses()
        {
            try
            {
                var tempAnalyses = await _stateService.LoadAnalysis(userId);
                Analyses.Clear();
                tempAnalyses.ForEach(m => Analyses.Add(m));
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }

        private async Task OnLoadChartData()
        {
            try
            {
                var chartData = await _stateService.LoadAnalysisChartData(userId, "HemoglobinTest");
                Entries.Clear();

                List<double> values = chartData.Data["Hemoglobin"];

                foreach (double value in values)
                {
                    Entries.Add(new ChartEntry((float)value) { Label = "Norm coef", ValueLabel = $"{value}", Color = SKColor.Parse("#2c3e50") });
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "An error occured: " + ex.Message, "Ok");
            }
        }
    }
}
