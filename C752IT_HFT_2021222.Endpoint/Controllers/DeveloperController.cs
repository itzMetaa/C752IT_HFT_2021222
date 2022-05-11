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
    public class DeveloperController : ControllerBase
    {
        IDeveloperLogic logic;

        public DeveloperController(IDeveloperLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<DeveloperController>
        [HttpGet]
        public IEnumerable<Developer> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<DeveloperController>/5
        [HttpGet("{id}")]
        public Developer Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<DeveloperController>
        [HttpPost]
        public void Create([FromBody] Developer value)
        {
            this.logic.Create(value);
        }

        // PUT api/<DeveloperController>/5
        [HttpPut("{id}")]
        public void Put([FromBody] Developer value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<DeveloperController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
