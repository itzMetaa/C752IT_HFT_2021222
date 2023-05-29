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
    public class MenuWindowViewModel
    {
        public ICommand GamesCommand { get; set; }
        public ICommand DevsCommand { get; set; }
        public ICommand PubsCommand { get; set; }
        public ICommand NonCrudCommand { get; set; }
        public ICommand ExitCommand { get; set; }


        public MenuWindowViewModel()
        {
            #region Commands

            GamesCommand = new RelayCommand
                    (() =>
                    {
                        _ = new MainWindow().ShowDialog();
                    }
                    );

            DevsCommand = new RelayCommand
                    (() =>
                    {
                        _ = new MainWindow().ShowDialog();
                    }
                    );

            PubsCommand = new RelayCommand
                    (() =>
                    {
                        _ = new MainWindow().ShowDialog();
                    }
                    );

            NonCrudCommand = new RelayCommand
                    (() =>
                    {
                        _ = new MainWindow().ShowDialog();
                    }
                    );

            ExitCommand = new RelayCommand
                    (() =>
                    {
                        Environment.Exit(0);
                    }
                    );

            #endregion
        }
    }
}
