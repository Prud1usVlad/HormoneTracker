using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Mobile.Services;
using Mobile.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mobile.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private string header;
        private string body;
        private Analysis analysis;
        private Datum displayData;
        private IAnalysisService _analysisService;

        public string Header
        {
            get { return header; }
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }
        public string Body
        {
            get { return body; }
            set
            {
                body = value;
                OnPropertyChanged("Body");
            }
        }
        public Analysis Analysis
        {
            get { return analysis; }
            set
            {
                analysis = value;
                OnPropertyChanged("Analysis");
            }
        }
        public Datum DisplayData 
        {
            get { return displayData; }
            set
            {
                displayData = value;
                OnPropertyChanged("DisplayData");
            }
        }
        public ICommand LoadAnalysis { get; set; }
        public ICommand SaveAnalysis { get; set; }

        public AboutViewModel()
        {
            Title = "About";
            Header = "Say Hi! to your health!";
            Body = "Start tracking your health state now! See your last analysis results below, and sent them to server, to share with your doctor and be availible to recive recomendations from us!";

            _analysisService = DependencyService.Get<IAnalysisService>();

            LoadAnalysis = new Command(async () => await OnLoad());
            SaveAnalysis = new Command(async () => await OnSave());

            Analysis = new Analysis();
            DisplayData = new Datum();
        }

        public async Task OnLoad()
        {
            int id = int.Parse(App.Current.Properties["userId"].ToString());
            Analysis = await _analysisService.GetLastLocalAnalysis(id);
            DisplayData = analysis.Data.First();
        }

        public async Task OnSave()
        {
            await _analysisService.AddAnalysis(Analysis);
        }

    }
}