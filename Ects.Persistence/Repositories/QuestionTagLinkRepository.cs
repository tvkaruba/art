using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionTagLinkRepository
        : RepositoryBase<QuestionTagLink, long>, IQuestionTagLinkRepository
    {
        public QuestionTagLinkRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}