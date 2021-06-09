using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class TestCommentLinkRepository
        : RepositoryBase<TestCommentLink, long>, ITestCommentLinkRepository
    {
        public TestCommentLinkRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}