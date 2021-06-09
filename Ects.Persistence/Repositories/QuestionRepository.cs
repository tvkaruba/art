using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionRepository
        : RepositoryBase<Question, long>, IQuestionRepository
    {
        public QuestionRepository(IDbTransaction transaction)
            : base(transaction) { }

        public async Task<IEnumerable<Question>> QueryQuestionsWithPaginationAsync(
            long page,
            long itemsOnPage,
            string searchTerm,
            long namespaceId,
            long? questionTypeId)
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate(
                @"select *
                       from Question as q
                   /**where**/
                      order by q.CreatedAtUtc
                     offset @page * @itemsOnPage rows
                      fetch next @itemsOnPage rows only");

            builder.AddParameters(new Dictionary<string, object>
            {
                // In web client pagination start from 1.
                { "page", --page },
                { "itemsOnPage", itemsOnPage },
                { "searchTerm", searchTerm },
                { "namespaceId", namespaceId },
                { "questionTypeId", questionTypeId },
                // By default output only active tasks.
                { "isActive", true }
            });

            builder.Where("q.IsActive = @isActive");
            builder.Where("q.NamespaceId = @namespaceId");
            if (questionTypeId.HasValue)
                builder.Where("q.QuestionTypeId = @questionTypeId");
            if (!string.IsNullOrWhiteSpace(searchTerm))
                builder.Where("(Name like @searchTerm or Body like @searchTerm)");

            var command = new CommandDefinition(
                template.RawSql,
                template.Parameters,
                Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<Question>(command);
        }
    }
}