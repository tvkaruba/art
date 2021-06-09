using System.Data;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories.Abstractions;

namespace Ects.Persistence.Repositories
{
    public class NamespaceRepository
        : RepositoryBase<Namespace, long>, INamespaceRepository
    {
        public NamespaceRepository(IDbTransaction transaction)
            : base(transaction) { }
    }
}