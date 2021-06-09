using Ects.Persistence.Models;

namespace Ects.Persistence.Repositories.Abstractions
{
    public interface ITestRepository
        : IRepository<Test, long> { }
}