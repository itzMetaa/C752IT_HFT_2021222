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
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        IPublisherLogic logic;

        public PublisherController(IPublisherLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<PublisherController>
        [HttpGet]
        public IEnumerable<Publisher> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<PublisherController>/5
        [HttpGet("{id}")]
        public Publisher Get(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<PublisherController>
        [HttpPost]
        public void Create([FromBody] Publisher value)
        {
            this.logic.Create(value);
        }

        // PUT api/<PublisherController>/5
        [HttpPut]
        public void Put([FromBody] Publisher value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<PublisherController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
