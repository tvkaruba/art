using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class ExamParticipantAnswerRepository
        : RepositoryBase<ExamParticipantAnswer, long>, IExamParticipantAnswerRepository
    {
        public ExamParticipantAnswerRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}