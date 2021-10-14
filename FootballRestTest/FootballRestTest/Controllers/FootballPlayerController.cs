using FootballRestTest.Manger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpgaveAS301;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FootballRestTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootballPlayerController : ControllerBase
    {
        IFootBallManger FBPmgr = new FootBallManger();
        // GET: api/<FootballPlayerController>
        [HttpGet]
        public IEnumerable<FootballPlayer> Get()
        {
            return FBPmgr.Get();
        }
     

        // GET api/<FootballPlayerController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(FBPmgr.Get(id));
            }
            catch (KeyNotFoundException knfe)
            {
                return NotFound(knfe.Message);
            }
        }
        [HttpGet]
        [Route("Name/{name}")]
        public IEnumerable<FootballPlayer> Get(string name)
        {
            // hack 
            //return new List<Pizza>(mgr.Get()).Find(p => p.Name.Contains(name));

            return FBPmgr.GetName(name);
        }

        // POST api/<FootballPlayerController>
        [HttpPost]
        public void Post([FromBody] FootballPlayer value)
        {
            FBPmgr.Create(value);
        }

        // PUT api/<FootballPlayerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] FootballPlayer value)
        {
             FBPmgr.Update(id, value);
        }

        // DELETE api/<FootballPlayerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            FBPmgr.Delete(id);
        }
    }
}
