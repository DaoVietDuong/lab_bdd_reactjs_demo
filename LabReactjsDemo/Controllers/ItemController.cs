using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabReactjsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public ItemController()
        {

        }
        // GET: api/<ItemController>
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return InMemoryDatabase.LabDbContext.Items;
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public Item Get(Guid id)
        {
            return InMemoryDatabase.LabDbContext.Items.SingleOrDefault(x => id == x.Id);
        }

        // POST api/<ItemController>
        [HttpPost]
        public void Post([FromBody] Item item)
        {
            item.Id = Guid.NewGuid();
            InMemoryDatabase.LabDbContext.Items.Add(item);
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Item item)
        {
            if (id != item.Id) return BadRequest();

            var instance = InMemoryDatabase.LabDbContext.Items.FirstOrDefault(x => item.Id == x.Id);

            if (instance == null) return BadRequest();

            InMemoryDatabase.LabDbContext.Items.Remove(instance);
            InMemoryDatabase.LabDbContext.Items.Add(item);
            return Ok();
        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var instance = InMemoryDatabase.LabDbContext.Items.FirstOrDefault(x => x.Id == id);

            if (instance == null) return BadRequest();

            InMemoryDatabase.LabDbContext.Items.Remove(instance);

            return Ok();
        }
    }
}
