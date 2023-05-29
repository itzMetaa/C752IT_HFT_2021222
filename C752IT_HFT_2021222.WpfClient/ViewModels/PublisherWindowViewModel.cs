using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.RestClient;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Publisher = C752IT_HFT_2021222.Models.Publisher;

namespace C752IT_HFT_2021222.WpfClient.ViewModels
{
    public class PublisherWindowViewModel : ObservableRecipient
    {
        #region properties
        public RestCollection<Publisher> Publishers { get; set; }

        public ICommand CreatePublisherCommand { get; set; }
        public ICommand UpdatePublisherCommand { get; set; }
        public ICommand DeletePublisherCommand { get; set; }

        private Publisher selectedPublisher;
        public Publisher SelectedPublisher
        {
            get { return selectedPublisher; }
            set
            {
                if (value != null)
                {
                    selectedPublisher = new Publisher()
                    {
                        Id = value.Id,
                        Name = value.Name
                    };
                }
                OnPropertyChanged();
                (DeletePublisherCommand as RelayCommand).NotifyCanExecuteChanged();
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

        public PublisherWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Publishers = new RestCollection<Publisher>("http://localhost:54503/", "publisher", "hub");

                #region Commands

                CreatePublisherCommand = new RelayCommand
                    (() =>
                    {
                        Publishers.Add(new Publisher()
                        {
                            Name = SelectedPublisher.Name
                        });
                    });
                UpdatePublisherCommand = new RelayCommand
                    (() =>
                    {
                        Publishers.Update(SelectedPublisher);
                    }, 
                    () => SelectedPublisher != null
                    );
                DeletePublisherCommand = new RelayCommand
                    (() =>
                    {
                        Publishers.Delete(SelectedPublisher.Id);
                        SelectedPublisher = new Publisher();
                    }, 
                    () => SelectedPublisher != null
                    );
                

                SelectedPublisher = new Publisher();

                #endregion

            }
        }

    }
}
