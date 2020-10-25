using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Server.Filters;
using Art.Web.Shared.Models.Person;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Art.Web.Server.Services.Abstractions;
using Microsoft.AspNetCore.Cors;

namespace Art.Web.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;

        private readonly IPersonService _personService;

        public PersonController(IJwtTokenService tokenService, IPersonService personService)
        {
            _tokenService = tokenService ?? throw new ArgumentException(nameof(tokenService));
            _personService = personService ?? throw new ArgumentException(nameof(personService));
        }

        [HttpGet]
        [Route("")]
        [Authorize(RolePolicies.Administrator)]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<PersonGet>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _personService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:long}", Name = nameof(GetPerson))]
        [Authorize(RolePolicies.User)]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PersonGet))]
        public async Task<IActionResult> GetPerson(long id)
        {
            var person = await _personService.GetAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [Route("")]
        [Authorize(RolePolicies.Administrator)]
        [SwaggerResponse(StatusCodes.Status201Created, "Created person id.", typeof(long))]
        public async Task<IActionResult> Registration([FromBody] PersonPost data)
        {
            var person = await _personService.CreateAsync(data);
            return CreatedAtRoute(nameof(GetPerson), new { id = person.Id }, person.Id);
        }

        [HttpPut]
        [Route("{id:long}")]
        [Authorize(RolePolicies.User)]
        [SwaggerResponse(StatusCodes.Status204NoContent, type: typeof(void))]
        public async Task<IActionResult> Update(long id, [FromBody] PersonPut data)
        {
            var person = await _personService.UpdateAsync(id, data);

            if (person == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:long}")]
        [Authorize(RolePolicies.Administrator)]
        [SwaggerResponse(StatusCodes.Status204NoContent, type: typeof(void))]
        public async Task<IActionResult> Delete(long id)
        {
            await _personService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        [EnableCors(PolicyName = "login")]
        [SwaggerResponse(StatusCodes.Status200OK, "Generated JWT token, users role and email.", typeof(PersonLoginGet))]
        public async Task<IActionResult> Login([FromBody] PersonLoginPost data)
        {
            var (role, id) = await _personService.GetPersonRoleAsync(data.Email, data.Password);

            if (role == null)
            {
                return BadRequest();
            }

            var token = _tokenService.BuildToken(role);
            return Ok(new PersonLoginGet { JwtToken = token, Role = role, Email = data.Email, Id = id });
        }
    }
}
