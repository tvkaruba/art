using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class TestTagLinkRepository
        : RepositoryBase<TestTagLink, long>, ITestTagLinkRepository
    {
        public TestTagLinkRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}