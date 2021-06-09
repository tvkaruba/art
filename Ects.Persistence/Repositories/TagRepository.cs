using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class TagRepository
        : RepositoryBase<Tag, long>, ITagRepository
    {
        public TagRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}