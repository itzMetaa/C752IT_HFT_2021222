using C752IT_HFT_2021222.Logic;
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
        IGameLogic logic;

        public StatController(IGameLogic logic)
        {
            this.logic = logic;
        }

        // GET api/<StatController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public GameInfo MostProfitableGame()
        {
            return this.logic.GetMostProfitableGame();
        }
        [HttpGet]
        public IEnumerable<GameInfo> GameRevenueInfo()
        {
            return this.logic.GetGameRevenueInfo();
        }

    }
}
