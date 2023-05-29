using C752IT_HFT_2021222.Endpoint.Services;
using C752IT_HFT_2021222.Logic;
using C752IT_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace C752IT_HFT_2021222.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic gameLogic;
        IDeveloperLogic developerLogic;
        IPublisherLogic publisherLogic;
        IHubContext<SignalRHub> hub;

        public StatController(IGameLogic logic, IDeveloperLogic devLogic, IPublisherLogic pubLogic, IHubContext<SignalRHub> hub)
        {
            this.gameLogic = logic;
            this.developerLogic = devLogic;
            this.publisherLogic = pubLogic;
            this.hub = hub;
        }

        //game logic
        [HttpGet]
        public GameInfo MostProfitableGame()
        {
            this.hub.Clients.All.SendAsync("Stat1");
            return this.gameLogic.GetMostProfitableGame();
        }
        [HttpGet]
        public IEnumerable<GameInfo> GameRevenueInfo()
        {
            this.hub.Clients.All.SendAsync("Stat2");
            return this.gameLogic.GetGameRevenueInfo();
        }
        [HttpGet]
        public double? AveragePriceOfGames()
        {
            this.hub.Clients.All.SendAsync("Stat3");
            return this.gameLogic.GetAveragePriceOfGames();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<GameType, int>> NumberOfGamesPerType()
        {
            this.hub.Clients.All.SendAsync("Stat4");
            return this.gameLogic.GetNumberOfGamesPerType();
        }
        //Publisher logic
        [HttpGet("{id}")]
        public IEnumerable<Game> GetGamesOfPublisher(int id)
        {
            this.hub.Clients.All.SendAsync("GetGamesOfPublisher");
            return this.publisherLogic.GamesOfPublisher(id);
        }
        //Developer logic
        [HttpGet("{id}")]
        public IEnumerable<Game> GetGamesOfDevelopers(int id)
        {
            this.hub.Clients.All.SendAsync("GetGamesOfDevelopers");
            return this.developerLogic.GamesOfDeveloper(id);
        }
    }
}
