using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class TestQuestionLinkRepository
        : RepositoryBase<TestQuestionLink, long>, ITestQuestionLinkRepository
    {
        public TestQuestionLinkRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}