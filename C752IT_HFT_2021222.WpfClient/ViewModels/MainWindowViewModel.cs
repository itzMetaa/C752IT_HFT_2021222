using C752IT_HFT_2021222.Models;
using C752IT_HFT_2021222.RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C752IT_HFT_2021222.WpfClient.ViewModels
{
    public class MainWindowViewModel
    {
        public RestCollection<Game> Games { get; set; }

        public MainWindowViewModel()
        {
            Games = new RestCollection<Game>("http://localhost:54503/", "game");
        }
    }
}
