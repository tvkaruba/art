using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionHistoryRepository
        : RepositoryBase<QuestionHistory, long>, IQuestionHistoryRepository
    {
        public QuestionHistoryRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}