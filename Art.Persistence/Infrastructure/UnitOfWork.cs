using System;
using System.Data;
using System.Data.SqlClient;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Persistence.Repositories;
using Art.Persistence.Repositories.Abstractions;

namespace Art.Persistence.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed;

        private IDbConnection _connection;

        private IDbTransaction _transaction;

        private IPersonRepository _personRepository;

        private ITaskRepository _taskRepository;

        private IVariantRepository _variantRepository;

        public IPersonRepository PersonRepository =>
            _personRepository ?? (_personRepository = new PersonRepository(_transaction));

        public ITaskRepository TaskRepository =>
            _taskRepository ?? (_taskRepository = new TaskRepository(_transaction));

        public IVariantRepository VariantRepository =>
            _variantRepository ?? (_variantRepository = new VariantRepository(_transaction));

        public UnitOfWork(IUnitOfWorkConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentException(nameof(configuration));
            }

            _connection = new SqlConnection(configuration.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _transaction?.Dispose();
            _transaction = null;
            _connection?.Dispose();
            _connection = null;

            _disposed = true;
        }

        public void Commit()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(UnitOfWork));
            }

            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }
    }
}
