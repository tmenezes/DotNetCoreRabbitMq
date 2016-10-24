using System.Collections.Generic;
using DotNetCoreRabbitMq.Queues;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotNetCoreRabbitMq.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IOptions<QueueConnectionSettings> _optionsAccessor;

        public ValuesController(IOptions<QueueConnectionSettings> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { _optionsAccessor.Value.HostName, "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
