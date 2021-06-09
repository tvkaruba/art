using System.Collections.Generic;
using Ects.Web.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;

        public TestController(
            IQuestionService questionService,
            ITestService testService)
        {
            _questionService = questionService;
            _testService = testService;
        }

        // GET: api/<TestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<TestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<TestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<TestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}