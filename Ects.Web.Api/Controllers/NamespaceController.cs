using System.Collections.Generic;
using Ects.Web.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamespaceController : ControllerBase
    {
        private readonly INamespaceService _namespaceService;

        public NamespaceController(INamespaceService namespaceService)
        {
            _namespaceService = namespaceService;
        }

        // GET: api/<NamespaceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<NamespaceController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<NamespaceController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<NamespaceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<NamespaceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}