using System.Collections.Generic;
using Ects.Web.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionHistoryController : ControllerBase
    {
        private readonly IQuestionHistoryService _questionHistoryService;

        public QuestionHistoryController(IQuestionHistoryService questionHistoryService)
        {
            _questionHistoryService = questionHistoryService;
        }

        // GET: api/<QuestionHistoryController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<QuestionHistoryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<QuestionHistoryController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<QuestionHistoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<QuestionHistoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}