using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabReactjsDemo.Command;
using LabReactjsDemo.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LabReactjsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseTestController : ControllerBase
    {
        private IMediator _mediator;
        public DatabaseTestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<ItemController>
        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            return await _mediator.Send(new ItemQuery());
        }

        // GET api/<ItemController>/5
        [HttpGet("{id}")]
        public Item GetById(Guid id)
        {
            return InMemoryDatabase.LabDbContext.Items.SingleOrDefault(x => id == x.Id);
        }

        // POST api/<ItemController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IEnumerable<CreateItemCommand> items)
        {
            var tasks = items.Select(item => _mediator.Send(item));

            await Task.WhenAll(tasks);
            return Ok();
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
        [HttpGet("Clear")]
        public IActionResult Delete()
        {
            InMemoryDatabase.LabDbContext.Items.Clear();

            return Ok();
        }
    }
}
