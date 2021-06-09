using Ects.Persistence.Models;

namespace Ects.Persistence.Repositories.Abstractions
{
    public interface ICommentRepository
        : IRepository<Comment, long> { }
}