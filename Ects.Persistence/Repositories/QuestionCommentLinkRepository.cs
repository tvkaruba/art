using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class QuestionCommentLinkRepository
        : RepositoryBase<QuestionCommentLink, long>, IQuestionCommentLinkRepository
    {
        public QuestionCommentLinkRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}