using System.Collections.Generic;
using Ects.Web.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ITestService _testService;
        private readonly ITagService _tagService;

        public TagController(
            IQuestionService questionService,
            ITestService testService,
            ITagService tagService)
        {
            _questionService = questionService;
            _testService = testService;
            _tagService = tagService;
        }

        // GET: api/<TagController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<TagController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TagController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<TagController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<TagController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}