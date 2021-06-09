using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Ects.Persistence.Abstractions;
using Ects.Persistence.Models;
using Ects.Web.Api.Services.Abstractions;
using Ects.Web.Api.Services.Infrastructure;
using Ects.Web.Api.Validators.Infrastructure.Abstractions;
using Ects.Web.Shared.Models.Person;

namespace Ects.Web.Api.Services
{
    public class PersonService : ValidatableCrudWithAuditServiceBase<Question, long, PersonPost, PersonPut, PersonGet>,
        IPersonService
    {
        public PersonService(
            IUnitOfWork uow,
            IMapper mapper,
            IValidationService<PersonPost> createValidationService,
            IValidationService<PersonPut> updateValidationService)
            : base(uow, mapper, createValidationService, updateValidationService) { }

        protected override Task<IEnumerable<Question>> GetAllInternalAsync()
        {
            throw new NotImplementedException();
        }

        protected override Task<Question> GetInternalAsync(long id)
        {
            throw new NotImplementedException();
        }

        protected override Task CreateInternalAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        protected override Task UpdateInternalAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        protected override Task DeleteInternalAsync(Question entity)
        {
            throw new NotImplementedException();
        }

        public Task<(string role, long id)> GetPersonRoleAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}