using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisAssociation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TennisAssociation.Controllers
{
    [Route("api/SignUp")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public bool Get([FromHeader] Player fan)
        {
            Console.WriteLine(fan.FirstName);
            return true;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] Player value)
        {
            
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
