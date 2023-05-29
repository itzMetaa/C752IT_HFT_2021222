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
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameLogic logic;
        IHubContext<SignalRHub> hub;
        public GameController(IGameLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }
        // GET: api/<GameController>
        [HttpGet]
        public IEnumerable<Game> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public Game Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<GameController>
        [HttpPost]
        public void Create([FromBody] Game value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("GameCreated",value);
        }

        // PUT api/<GameController>/5
        [HttpPut]
        public void Put([FromBody] Game value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("GameUpdated", value);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("GameDeleted", gameToDelete);
        }
    }
}
