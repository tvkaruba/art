using System.Collections.Generic;
using Ects.Web.Shared.QuestionTypes.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionTypeController : ControllerBase
    {
        private readonly IQuestionType _questionType;

        public QuestionTypeController(IQuestionType questionType)
        {
            _questionType = questionType;
        }

        // GET: api/<QuestionTypeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<QuestionTypeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<QuestionTypeController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<QuestionTypeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<QuestionTypeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}