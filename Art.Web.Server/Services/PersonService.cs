using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Persistence.Entities;
using Art.Web.Server.Services.Abstractions;
using Art.Web.Server.Services.Infrastructure;
using Art.Web.Server.Validators.Infrastructure.Abstractions;
using Art.Web.Shared.Models.Person;
using Task = System.Threading.Tasks.Task;

namespace Art.Web.Server.Services
{
    public class PersonService : ValidatableCrudWithAuditServiceBase<Person, long, PersonPost, PersonPut, PersonGet>, IPersonService
    {
        public PersonService(
            IUnitOfWork uow,
            IMapper mapper,
            IValidationService<PersonPost> createValidationService,
            IValidationService<PersonPut> updateValidationService)
            : base(uow, mapper, createValidationService, updateValidationService)
        {
        }

        public async Task<(string role, long id)> GetPersonRoleAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException(nameof(email));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password));
            }

            var person = await UnitOfWork.PersonRepository.QueryPersonByEmailAsync(email);

            if (person == null || person.Password != password)
            {
                return (null, 0);
            }

            return (person.PersonRoleId.ToString(), person.Id);
        }

        protected override async Task<IEnumerable<Person>> GetAllInternalAsync()
        {
            return await UnitOfWork.PersonRepository.GetAllAsync();
        }

        protected override async Task<Person> GetInternalAsync(long id)
        {
            return await UnitOfWork.PersonRepository.GetAsync(id);
        }

        protected override async Task CreateInternalAsync(Person entity)
        {
            await UnitOfWork.PersonRepository.CreateAsync(entity);
            UnitOfWork.Commit();
        }

        protected override async Task UpdateInternalAsync(Person entity)
        {
            await UnitOfWork.PersonRepository.UpdateAsync(entity);
            UnitOfWork.Commit();
        }

        protected override async Task DeleteInternalAsync(Person entity)
        {
            await UnitOfWork.PersonRepository.DeleteAsync(entity);
            UnitOfWork.Commit();
        }
    }
}