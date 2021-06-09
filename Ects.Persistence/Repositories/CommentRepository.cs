using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class CommentRepository
        : RepositoryBase<Comment, long>, ICommentRepository
    {
        public CommentRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}