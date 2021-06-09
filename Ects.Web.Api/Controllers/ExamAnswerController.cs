using System.Collections.Generic;
using Ects.Web.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamAnswerController : ControllerBase
    {
        private readonly IExamService _examService;
        private readonly IExamAnswerService _examAnswerService;
        private readonly IExamParticipantService _examParticipantService;

        public ExamAnswerController(
            IExamAnswerService examAnswerService,
            IExamService examService,
            IExamParticipantService examParticipantService)
        {
            _examParticipantService = examParticipantService;
            _examAnswerService = examAnswerService;
            _examService = examService;
        }

        // GET: api/<ExamParticipantAnswerController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET api/<ExamParticipantAnswerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExamParticipantAnswerController>
        [HttpPost]
        public void Post([FromBody] string value) { }

        // PUT api/<ExamParticipantAnswerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) { }

        // DELETE api/<ExamParticipantAnswerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}