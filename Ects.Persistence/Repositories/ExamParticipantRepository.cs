using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class ExamParticipantRepository
        : RepositoryBase<ExamParticipant, long>, IExamParticipantRepository
    {
        public ExamParticipantRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}