using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionTypeRepository
        : RepositoryBase<QuestionType, long>, IQuestionTypeRepository
    {
        public QuestionTypeRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}