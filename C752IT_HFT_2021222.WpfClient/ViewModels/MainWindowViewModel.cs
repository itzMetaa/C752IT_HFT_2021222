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
    public class MainWindowViewModel : ObservableRecipient
    {
        public RestCollection<Game> Games { get; set; }

        private Game selectedGame;

        public Game SelectedGame
        {
            get { return selectedGame; }
            set {
                if (value != null)
                {
                    selectedGame = new Game()
                    {
                        Title = value.Title,
                        Id = value.Id,
                        CopiesSold = value.CopiesSold,
                        Description = value.Description,
                        Developer = value.Developer,
                        DeveloperId = value.DeveloperId,
                        Price = value.Price,
                        Rating = value.Rating,
                        Type = value.Type 
                    };
                    OnPropertyChanged();
                    (DeleteGameCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }


        public ICommand CreateGameCommand { get; set; }
        public ICommand DeleteGameCommand { get; set; }
        public ICommand UpdateGameCommand { get; set; }

        public static bool IsInDesignMode
        {
            get 
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Games = new RestCollection<Game>("http://localhost:54503/", "game", "hub");

                CreateGameCommand = new RelayCommand(() =>
                {
                    Games.Add(new Game()
                    {
                        Title = SelectedGame.Title
                    });
                });

                UpdateGameCommand = new RelayCommand(() =>
                {
                    Games.Update(SelectedGame);
                });

                DeleteGameCommand = new RelayCommand(() =>
                {
                    Games.Delete(selectedGame.Id);
                },
                () =>
                {
                    return SelectedGame != null;
                });
                SelectedGame = new Game();
            }
        }
    }
}
