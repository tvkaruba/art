using System;
using System.Collections.Generic;
using Ects.Web.Api.Services.Abstractions;
using Ects.Web.Shared.Models.Account;
using Microsoft.AspNetCore.Mvc;

namespace Ects.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IEnumerable<AccountGet> GetAll()
        {
            _accountService.GetAllAsync();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            _accountService.GetAsync(Id);
        }

        // POST api/<AccountController>
        [HttpPost]
        public void Create([FromBody] string value) { }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) { }
    }
}