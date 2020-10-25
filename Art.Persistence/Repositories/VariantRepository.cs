using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Art.Persistence.Entities;
using Art.Persistence.Infrastructure;
using Art.Persistence.Repositories.Abstractions;
using Dapper;
using ArtTask = Art.Persistence.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Persistence.Repositories
{
    public class VariantRepository : RepositoryBase<Variant, long>, IVariantRepository
    {
        public VariantRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<IEnumerable<ArtTask>> QueryTasksByVariantIdAsync(long variantId)
        {
            var command = new CommandDefinition(
                @"select *
                    from Task as t
                   where t.Id in ( select TaskId
                                     from VariantTask as vt
                                    where vt.VariantId = @variantId )",
                new { variantId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<ArtTask>(command);
        }

        public async Task UpdateTasksByVariantIdAsync(long variantId, IEnumerable<long> taskIds)
        {
            taskIds = taskIds.ToList();

            var existingTaskIds = (await QueryTasksByVariantIdAsync(variantId)).Select(t => t.Id).ToList();
            var taskIdsToRemove = existingTaskIds.Except(taskIds.Intersect(existingTaskIds));

            var deleteCommand = new CommandDefinition(
                @"delete from VariantTask
                   where VariantId = @variantId
                     and TaskId = @taskId",
                taskIdsToRemove.Select(t => new { taskId = t, variantId }),
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            _ = await Connection.ExecuteAsync(deleteCommand);

            var taskIdsToAdd = taskIds.Except(taskIds.Intersect(existingTaskIds));

            var insertCommand = new CommandDefinition(
                @"insert into VariantTask
                         (VariantId, TaskId)
                  values (@variantId, @taskId)",
                taskIdsToAdd.Select(t => new { taskId = t, variantId }),
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            _ = await Connection.ExecuteAsync(insertCommand);
        }
    }
}
