using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class AccountRepository
        : RepositoryBase<Account, long>, IAccountRepository
    {
        public AccountRepository(IDbTransaction transaction)
            : base(transaction) { }

        public async Task<Account> QueryAccountByOidAsync(Guid oid)
        {
            var command = new CommandDefinition(
                @"select *
                    from Account as a
                   where a.Oid = @oid",
                new { oid },
                Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QuerySingleAsync<Account>(command);
        }

        public async Task<IEnumerable<Account>> QueryAccountsByStudyGroupAsync(string studyGroup)
        {
            var command = new CommandDefinition(
                @"select *
                    from Account as a
                   where a.StudyGroup = @studyGroup",
                new { studyGroup },
                Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<Account>(command);
        }

        public async Task<IEnumerable<string>> QueryStudyGroupsAsync()
        {
            var command = new CommandDefinition(
                @"select distinct StudyGroup
                    from Account",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }
    }
}