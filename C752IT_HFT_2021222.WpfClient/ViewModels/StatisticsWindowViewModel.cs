using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.RestClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace C752IT_HFT_2021222.WpfClient.ViewModels
{
    internal class StatisticsWindowViewModel : ObservableRecipient
    {
        public GameInfo MostProfitableGame { get; }
        public double AveragePriceOfGames { get; }
        public List<GameInfo> GameRevenueInfo { get; }
        public List<KeyValuePair<GameType, int>> NumberOfGamesPerType { get; }
        private List<Game> gamesOfPublisher;
        public List<Game> GamesOfPublisher { get { return gamesOfPublisher; } 
            set {
                SetProperty(ref gamesOfPublisher, value); 
                OnPropertyChanged(); 
            } 
        }
        private List<Game> gamesOfDeveloper;
        public List<Game> GamesOfDeveloper
        {
            get { return gamesOfDeveloper; }
            set
            {
                SetProperty(ref gamesOfDeveloper, value);
                OnPropertyChanged();
            }
        }
        public ICommand PublisherListCommand { get; }
        public ICommand DeveloperListCommand { get; }

        public RestCollection<Publisher> Publishers { get; set; }
        public RestCollection<Developer> Developers { get; set; }

        public StatisticsWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Developers = new RestCollection<Developer>("http://localhost:54503/", "developer", "hub");
                Publishers = new RestCollection<Publisher>("http://localhost:54503/", "publisher", "hub");

                MostProfitableGame = new RestService("http://localhost:54503/stat/", "MostProfitableGame").GetSingle<GameInfo>("MostProfitableGame");
                AveragePriceOfGames = new RestService("http://localhost:54503/stat/", "AveragePriceOfGames").GetSingle<double>("AveragePriceOfGames");
                GameRevenueInfo = new RestService("http://localhost:54503/stat/", "GameRevenueInfo").Get<GameInfo>("GameRevenueInfo");
                NumberOfGamesPerType = new RestService("http://localhost:54503/stat/", "NumberOfGamesPerType").Get<KeyValuePair<GameType, int>>("NumberOfGamesPerType");

                PublisherListCommand = new RelayCommand
                   (() =>
                   {
                       GamesOfPublisher = new RestService("http://localhost:54503/").Get<List<Game>>(SelectedPublisher.Id, "stat/GetGamesOfPublisher");
                   }, ()=> SelectedPublisher != null
                );
                DeveloperListCommand = new RelayCommand
                   (() =>
                   {
                       GamesOfDeveloper = new RestService("http://localhost:54503/").Get<List<Game>>(SelectedDeveloper.Id, "stat/GetGamesOfDevelopers");
                   }, () => SelectedDeveloper != null
                );

                GamesOfPublisher = new List<Game>();
                GamesOfDeveloper = new List<Game>();
                SelectedPublisher = new Publisher();
                SelectedDeveloper = new Developer();
            }
        }

        private Developer selectedDeveloper;
        public Developer SelectedDeveloper
        {
            get { return selectedDeveloper; }
            set
            {
                SetProperty(ref selectedDeveloper, value);
            }
        }

        private Publisher selectedPublisher;
        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                SetProperty(ref selectedPublisher, value);
            }
        }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}
