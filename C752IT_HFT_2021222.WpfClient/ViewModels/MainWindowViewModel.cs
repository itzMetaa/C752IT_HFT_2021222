using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.RestClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace C752IT_HFT_2021222.WpfClient.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Game> Games { get; set; }

        private Game selectedGame;

        public Game SelectedGame
        {
            get { return selectedGame; }
            set { 
                SetProperty(ref selectedGame, value);
                (DeleteGameCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }


        public ICommand CreateGameCommand { get; set; }
        public ICommand DeleteGameCommand { get; set; }
        public ICommand UpdateGameCommand { get; set; }

        public MainWindowViewModel()
        {
            Games = new RestCollection<Game>("http://localhost:54503/", "game");

            CreateGameCommand = new RelayCommand(() =>
            {
                Games.Add(new Game()
                {
                    Title = "Add test"
                });
            });

            UpdateGameCommand = new RelayCommand(() =>
            {
                Games.Add(new Game()
                {
                    Title = "Add test"
                });
            });

            DeleteGameCommand = new RelayCommand(() =>
            {
                Games.Delete(selectedGame.Id);
            },
            () =>
            { 
                return SelectedGame != null;
            });

        }
    }
}
