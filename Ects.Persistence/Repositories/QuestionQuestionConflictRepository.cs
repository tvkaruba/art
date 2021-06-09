using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionQuestionConflictRepository
        : RepositoryBase<QuestionQuestionConflict, long>, IQuestionQuestionConflictRepository
    {
        public QuestionQuestionConflictRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}