using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class TestRepository
        : RepositoryBase<Test, long>, ITestRepository
    {
        public TestRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}