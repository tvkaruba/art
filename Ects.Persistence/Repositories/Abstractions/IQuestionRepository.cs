using System.Collections.Generic;
using System.Threading.Tasks;
using Ects.Persistence.Models;

namespace Ects.Persistence.Repositories.Abstractions
{
    public interface IQuestionRepository
        : IRepository<Question, long>
    {
        Task<IEnumerable<Question>> QueryQuestionsWithPaginationAsync(
            long page,
            long itemsOnPage,
            string searchTerm,
            long namespaceId,
            long? questionTypeId);
    }
}