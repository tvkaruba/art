using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Repositories.Abstractions
{
    public abstract class RepositoryBase<TEntity, TKey>
        : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        protected IDbTransaction Transaction { get; }

        protected IDbConnection Connection => Transaction.Connection;

        protected RepositoryBase(IDbTransaction transaction)
        {
            Transaction = transaction ?? throw new ArgumentException(nameof(transaction));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Connection.GetAllAsync<TEntity>(Transaction);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await Connection.GetAsync<TEntity>(id, Transaction);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Connection.InsertAsync(entity, Transaction);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Connection.UpdateAsync(entity, Transaction);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await Connection.DeleteAsync(entity, Transaction);
        }
    }
}