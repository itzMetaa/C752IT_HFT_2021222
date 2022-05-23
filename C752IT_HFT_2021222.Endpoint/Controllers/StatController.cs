using C752IT_HFT_2021222.Logic;
using C752IT_HFT_2021222.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace C752IT_HFT_2021222.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic gameLogic;
        IDeveloperLogic developerLogic;
        IPublisherLogic publisherLogic;

        public StatController(IGameLogic logic, IDeveloperLogic devLogic, IPublisherLogic pubLogic)
        {
            this.gameLogic = logic;
            this.developerLogic = devLogic;
            this.publisherLogic = pubLogic;
        }
        [HttpGet]
        public GameInfo MostProfitableGame()
        {
            return this.gameLogic.GetMostProfitableGame();
        }
        [HttpGet]
        public IEnumerable<GameInfo> GameRevenueInfo()
        {
            return this.gameLogic.GetGameRevenueInfo();
        }
        [HttpGet]
        public double? AveragePriceOfGames()
        {
            return this.gameLogic.GetAveragePriceOfGames();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<GameType, int>> NumberOfGamesPerType()
        {
            return this.gameLogic.GetNumberOfGamesPerType();
        }
        [HttpGet("{id}")]
        public IEnumerable<Game> GetGamesOfPublisher(int id)
        {
            return this.publisherLogic.GamesOfPublisher(id);
        }
        [HttpGet("{id}")]
        public IEnumerable<Game> GetGamesOfDevelopers(int id)
        {
            return this.developerLogic.GamesOfDeveloper(id);
        }
    }
}
