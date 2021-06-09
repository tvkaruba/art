using System;
using Art.Persistence.Repositories.Abstractions;

namespace Art.Persistence.Infrastructure.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IPersonRepository PersonRepository { get; }

        ITaskRepository TaskRepository { get; }

        IVariantRepository VariantRepository { get; }

        void Commit();
    }
}
