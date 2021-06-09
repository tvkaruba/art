using Dapper;
using System.Data;
using System.Threading.Tasks;
using Art.Persistence.Entities;
using Art.Persistence.Infrastructure;
using Art.Persistence.Repositories.Abstractions;

namespace Art.Persistence.Repositories
{
    public class PersonRepository : RepositoryBase<Person, long>, IPersonRepository
    {
        public PersonRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<Person> QueryPersonByEmailAsync(string email)
        {
            var command = new CommandDefinition(
                @"select *
                    from Person as p
                   where p.Email = @email",
                new { email },
                Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QuerySingleAsync<Person>(command);
        }
    }
}
