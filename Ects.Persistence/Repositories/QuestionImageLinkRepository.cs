using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionImageLinkRepository
        : RepositoryBase<QuestionImageLink, long>, IQuestionImageLinkRepository
    {
        public QuestionImageLinkRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}