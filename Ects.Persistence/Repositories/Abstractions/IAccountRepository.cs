using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ects.Persistence.Models;

namespace Ects.Persistence.Repositories.Abstractions
{
    public interface IAccountRepository
        : IRepository<Account, long>
    {
        Task<Account> QueryAccountByOidAsync(Guid oid);

        Task<IEnumerable<Account>> QueryAccountsByStudyGroupAsync(string studyGroup);

        Task<IEnumerable<string>> QueryStudyGroupsAsync();
    }
}