using C752IT_HFT_2021222.RestClient;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using C752IT_HFT_2021222.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace C752IT_HFT_2021222.WpfClient.ViewModels
{
    internal class DeveloperWindowViewModel : ObservableRecipient
    {
        public DeveloperWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Developers = new RestCollection<Developer>("http://localhost:54503/", "developer", "hub");
                Publishers = new RestCollection<Publisher>("http://localhost:54503/", "publisher", "hub");

                #region Commands

                CreateDeveloperCommand = new RelayCommand
                   (() =>
                   {
                       Developers.Add(new Developer()
                       {
                           Name = SelectedDeveloper.Name,
                           PublisherId = SelectedPublisher.Id
                       });
                   }
                   );

                UpdateDeveloperCommand = new RelayCommand
                    (() =>
                    {
                        SelectedDeveloper.PublisherId = SelectedPublisher.Id;
                        Developers.Update(SelectedDeveloper);
                    }, 
                    () => SelectedDeveloper != null
                    );

                DeleteDeveloperCommand = new RelayCommand
                    (() =>
                    {
                        Developers.Delete(SelectedDeveloper.Id);
                    }, 
                    () => SelectedDeveloper != null
                    );

                SelectedDeveloper = new Developer();
                SelectedPublisher = new Publisher();
                #endregion
            }
        }

        #region properties

        public RestCollection<Developer> Developers { get; set; }
        public RestCollection<Publisher> Publishers { get; set; }

        public ICommand CreateDeveloperCommand { get; set; }
        public ICommand UpdateDeveloperCommand { get; set; }
        public ICommand DeleteDeveloperCommand { get; set; }

        private Developer selectedDeveloper;
        public Developer SelectedDeveloper
        {
            get { return selectedDeveloper; }
            set
            {
                //SetProperty(ref _selectedRoom, value);
                if (value != null)
                {
                    selectedDeveloper = new Developer()
                    {
                        Games = value.Games,
                        Id = value.Id,
                        Publisher = value.Publisher,
                        Name = value.Name,
                        PublisherId = value.PublisherId,
                        TeamSize = value.TeamSize
                    };
                    OnPropertyChanged();
                    (DeleteDeveloperCommand as RelayCommand).NotifyCanExecuteChanged();
                }
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

        #endregion


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
