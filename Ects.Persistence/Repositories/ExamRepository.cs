using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class ExamRepository
        : RepositoryBase<Exam, long>, IExamRepository
    {
        public ExamRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}