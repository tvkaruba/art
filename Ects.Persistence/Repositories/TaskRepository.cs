using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Art.Persistence.Entities;
using Art.Persistence.Infrastructure;
using Art.Persistence.ReferenceData;
using Art.Persistence.Repositories.Abstractions;
using Dapper;
using ArtTask = Art.Persistence.Entities.Task;
using Task = System.Threading.Tasks.Task;

namespace Art.Persistence.Repositories
{
    public class TaskRepository : RepositoryBase<ArtTask, long>, ITaskRepository
    {
        public TaskRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<long> QueryTasksCountWithFiltersAsync(string searchTerm, Module? module, TaskType? type)
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate(
                @"select count(*)
                       from Task
                   /**where**/");

            builder.AddParameters(new Dictionary<string, object>
            {
                { "searchTerm",  searchTerm },
                { "module", module },
                { "type", type },
                // By default output only active tasks.
                { "isActive", true }
            });

            builder.Where("IsActive = @isActive");

            if (module.HasValue)
            {
                builder.Where("ModuleId = @module");
            }

            if (type.HasValue)
            {
                builder.Where("TaskTypeId = @type");
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                builder.Where("(Name like @searchTerm or Body like @searchTerm)");
            }

            var command = new CommandDefinition(
                template.RawSql,
                template.Parameters,
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryFirstAsync<long>(command);
        }

        public async Task<IEnumerable<string>> QueryAllTopicsAsync()
        {
            var command = new CommandDefinition(
                @"select Code
                    from Topic",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }

        public async Task<IEnumerable<string>> QueryTopicsByTaskIdAsync(long taskId)
        {
            var command = new CommandDefinition(
                @"select Code
                    from Topic as t
                   where t.Id in ( select TopicId
                                     from TaskTopic as tt
                                    where tt.TaskId = @taskId )",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }

        public async Task UpdateTopicsByTaskIdAsync(long taskId, IEnumerable<string> topics)
        {
            topics = topics.ToList();

            var existingTopics = (await QueryTopicsByTaskIdAsync(taskId)).ToList();

            var topicsToRemove = existingTopics.Except(topics.Intersect(existingTopics)).ToList();
            if (topicsToRemove.Any())
            {
                var builder = new SqlBuilder();
                var selectTopicIdsToRemoveCommandTemplate = builder.AddTemplate(
                    @"select Id
                           from Topic
                       /**where**/");

                var args = new Dictionary<string, object>();
                for (var i = 0; i < topicsToRemove.Count(); ++i)
                {
                    var argId = $"arg{i}";
                    builder.OrWhere($"Code = @{argId}");
                    args.Add(argId, topicsToRemove[i]);
                }

                builder.AddParameters(args);

                var selectTopicIdsToRemoveCommand = new CommandDefinition(
                    selectTopicIdsToRemoveCommandTemplate.RawSql,
                    selectTopicIdsToRemoveCommandTemplate.Parameters,
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                var topicIdsToRemove = (await Connection.QueryAsync<long>(selectTopicIdsToRemoveCommand)).ToList();

                var deleteReferencesToRedundantTopicsCommand = new CommandDefinition(
                    @"delete from TaskTopic
                       where TaskId = @taskId
                         and TopicId = @topicId",
                    topicIdsToRemove.Select(t => new { topicId = t, taskId }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(deleteReferencesToRedundantTopicsCommand);
            }

            var selectAllReferencedTopicIdsCommand = new CommandDefinition(
                @"select TopicId
                    from TaskTopic",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            var allReferencedTopicIds = (await Connection.QueryAsync<long>(selectAllReferencedTopicIdsCommand)).Distinct().ToList();

            var selectAllExistingTopicIdsCommand = new CommandDefinition(
                @"select Id
                    from Topic",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            var allExistingTopicIds = (await Connection.QueryAsync<long>(selectAllExistingTopicIdsCommand)).ToList();

            var redundantTopicIds = allExistingTopicIds.Except(allReferencedTopicIds.Intersect(allExistingTopicIds)).ToList();
            if (redundantTopicIds.Any())
            {
                var deleteRedundantTopicsCommand = new CommandDefinition(
                    @"delete from Topic
                       where Id = @topicId",
                    redundantTopicIds.Select(t => new { topicId = t }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(deleteRedundantTopicsCommand);
            }

            var allExistingTopics = (await QueryAllTopicsAsync()).ToList();

            var newTopics = topics.Except(topics.Intersect(allExistingTopics)).ToList();
            if (newTopics.Any())
            {
                var insertNewTopicsCommand = new CommandDefinition(
                    @"insert into Topic
                             (Code)
                      values (@topic)",
                    newTopics.Select(t => new {topic = t}),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(insertNewTopicsCommand);
            }

            var topicsToAdd = topics.Except(topics.Intersect(existingTopics)).ToList();
            if (topicsToAdd.Any())
            {
                var builder = new SqlBuilder();
                var selectTopicIdsToAddCommandTemplate = builder.AddTemplate(
                    @"select Id
                           from Topic
                       /**where**/");

                var args = new Dictionary<string, object>();
                for (var i = 0; i < topicsToAdd.Count(); ++i)
                {
                    var argId = $"arg{i}";
                    builder.OrWhere($"Code = @{argId}");
                    args.Add(argId, topicsToAdd[i]);
                }

                builder.AddParameters(args);

                var selectTopicIdsToAddCommand = new CommandDefinition(
                    selectTopicIdsToAddCommandTemplate.RawSql,
                    selectTopicIdsToAddCommandTemplate.Parameters,
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                var topicIdsToAdd = (await Connection.QueryAsync<long>(selectTopicIdsToAddCommand)).ToList();

                var insertReferencesToAddedTopicsCommand = new CommandDefinition(
                    @"insert into TaskTopic
                             (TaskId, TopicId)
                      values (@taskId, @topicId)",
                    topicIdsToAdd.Select(t => new { topicId = t, taskId }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(insertReferencesToAddedTopicsCommand);
            }
        }

        public async Task<IEnumerable<string>> QueryAllTagsAsync()
        {
            var command = new CommandDefinition(
                @"select Code
                    from Tag",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }

        public async Task<IEnumerable<string>> QueryTagsByTaskIdAsync(long taskId)
        {
            var command = new CommandDefinition(
                @"select Code
                    from Tag as t
                   where t.Id in ( select TagId
                                     from TaskTag as tt
                                    where tt.TaskId = @taskId )",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }

        public async Task UpdateTagsByTaskIdAsync(long taskId, IEnumerable<string> tags)
        {
            tags = tags.ToList();

            var existingTags = (await QueryTagsByTaskIdAsync(taskId)).ToList();

            var tagsToRemove = existingTags.Except(tags.Intersect(existingTags)).ToList();
            if (tagsToRemove.Any())
            {
                var builder = new SqlBuilder();
                var selectTagIdsToRemoveCommandTemplate = builder.AddTemplate(
                    @"select Id
                           from Tag
                       /**where**/");

                var args = new Dictionary<string, object>();
                for (var i = 0; i < tagsToRemove.Count(); ++i)
                {
                    var argId = $"arg{i}";
                    builder.OrWhere($"Code = @{argId}");
                    args.Add(argId, tagsToRemove[i]);
                }

                builder.AddParameters(args);

                var selectTagIdsToRemoveCommand = new CommandDefinition(
                    selectTagIdsToRemoveCommandTemplate.RawSql,
                    selectTagIdsToRemoveCommandTemplate.Parameters,
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                var tagIdsToRemove = (await Connection.QueryAsync<long>(selectTagIdsToRemoveCommand)).ToList();

                var deleteReferencesToRedundantTagsCommand = new CommandDefinition(
                    @"delete from TaskTag
                       where TaskId = @taskId
                         and TagId = @tagId",
                    tagIdsToRemove.Select(t => new { tagId = t, taskId }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(deleteReferencesToRedundantTagsCommand);
            }

            var selectAllReferencedTagIdsCommand = new CommandDefinition(
                @"select TagId
                    from TaskTag",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            var allReferencedTagIds = (await Connection.QueryAsync<long>(selectAllReferencedTagIdsCommand)).Distinct().ToList();

            var selectAllExistingTagIdsCommand = new CommandDefinition(
                @"select Id
                    from Tag",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            var allExistingTagIds = (await Connection.QueryAsync<long>(selectAllExistingTagIdsCommand)).ToList();

            var redundantTagIds = allExistingTagIds.Except(allReferencedTagIds.Intersect(allExistingTagIds)).ToList();
            if (redundantTagIds.Any())
            {
                var deleteRedundantTagsCommand = new CommandDefinition(
                    @"delete from Tag
                       where Id = @tagId",
                    redundantTagIds.Select(t => new { tagId = t }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(deleteRedundantTagsCommand);
            }

            var allExistingTags = (await QueryAllTagsAsync()).ToList();

            var newTags = tags.Except(tags.Intersect(allExistingTags)).ToList();
            if (newTags.Any())
            {
                var insertNewTagsCommand = new CommandDefinition(
                    @"insert into Tag
                             (Code)
                      values (@tag)",
                    newTags.Select(t => new { tag = t }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(insertNewTagsCommand);
            }

            var tagsToAdd = tags.Except(tags.Intersect(existingTags)).ToList();
            if (tagsToAdd.Any())
            {
                var builder = new SqlBuilder();
                var selectTagIdsToAddCommandTemplate = builder.AddTemplate(
                    @"select Id
                           from Tag
                       /**where**/");

                var args = new Dictionary<string, object>();
                for (var i = 0; i < tagsToAdd.Count(); ++i)
                {
                    var argId = $"arg{i}";
                    builder.OrWhere($"Code = @{argId}");
                    args.Add(argId, tagsToAdd[i]);
                }

                builder.AddParameters(args);

                var selectTagIdsToAddCommand = new CommandDefinition(
                    selectTagIdsToAddCommandTemplate.RawSql,
                    selectTagIdsToAddCommandTemplate.Parameters,
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                var tagIdsToAdd = (await Connection.QueryAsync<long>(selectTagIdsToAddCommand)).ToList();

                var insertReferencesToAddedTagsCommand = new CommandDefinition(
                    @"insert into TaskTag
                             (TaskId, TagId)
                      values (@taskId, @tagId)",
                    tagIdsToAdd.Select(t => new { tagId = t, taskId }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(insertReferencesToAddedTagsCommand);
            }
        }

        public async Task<IEnumerable<string>> QueryAnswersByTaskIdAsync(long taskId)
        {
            var command = new CommandDefinition(
                @"select Code
                    from Answer
                   where TaskId = @taskId
                   order by Id asc",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }

        public async Task UpdateAnswersByTaskIdAsync(long taskId, IEnumerable<string> answers)
        {
            answers = answers.ToList();

            var existingAnswers = (await QueryAnswersByTaskIdAsync(taskId)).ToList();

            if (answers.Except(existingAnswers).Any())
            {
                var deleteCommand = new CommandDefinition(
                    @"delete from Answer
                       where TaskId = @taskId",
                    new { taskId },
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                await Connection.ExecuteAsync(deleteCommand);

                var insertCommand = new CommandDefinition(
                    @"insert into Answer
                             (TaskId, Code)
                      values (@taskId, @code)",
                    answers.Select(a => new { code = a, taskId }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(insertCommand);
            }
        }

        public async Task<IEnumerable<string>> QueryRightsByTaskIdAsync(long taskId)
        {
            var command = new CommandDefinition(
                @"select Code
                    from [Right]
                   where TaskId = @taskId
                   order by Id asc",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<string>(command);
        }

        public async Task UpdateRightsByTaskIdAsync(long taskId, IEnumerable<string> rights)
        {
            rights = rights.ToList();

            var existingRights = (await QueryRightsByTaskIdAsync(taskId)).ToList();

            if (rights.Except(existingRights).Any())
            {
                var deleteCommand = new CommandDefinition(
                    @"delete from [Right]
                       where TaskId = @taskId",
                    new { taskId },
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                await Connection.ExecuteAsync(deleteCommand);

                var insertCommand = new CommandDefinition(
                    @"insert into [Right]
                             (TaskId, Code)
                      values (@taskId, @code)",
                    rights.Select(r => new { code = r, taskId }),
                    transaction: Transaction,
                    flags: CommandFlags.NoCache);

                _ = await Connection.ExecuteAsync(insertCommand);
            }
        }

        public async Task<IEnumerable<ArtTask>> QueryTasksWithPaginationAndFiltersAsync(
            long page,
            long itemsOnPage,
            string searchTerm,
            Module? module,
            TaskType? type)
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate(
                @"select *
                    from Task
                /**where**/
                   order by id
                  offset @page * @itemsOnPage rows
                   fetch next @itemsOnPage rows only");

            builder.AddParameters(new Dictionary<string, object>
            {
                // In web pagination start from 1.
                { "page", --page },
                { "itemsOnPage", itemsOnPage },
                { "searchTerm",  searchTerm },
                { "module", module },
                { "type", type },
                // By default output only active tasks.
                { "isActive", true }
            });

            builder.Where("IsActive = @isActive");

            if (module.HasValue)
            {
                builder.Where("ModuleId = @module");
            }

            if (type.HasValue)
            {
                builder.Where("TaskTypeId = @type");
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                builder.Where("(Name like @searchTerm or Body like @searchTerm)");
            }

            var command = new CommandDefinition(
                template.RawSql,
                template.Parameters,
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<ArtTask>(command);
        }

        public async Task<IEnumerable<TaskHistory>> QueryTaskHistoryByTaskIdAsync(long taskId)
        {
            var command = new CommandDefinition(
                @"select *
                    from TaskHistory
                   where Id = @taskId",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<TaskHistory>(command);
        }

        public async Task AddTaskConflictsAsync(long taskId, IEnumerable<long> conflictedIds)
        {
            var deleteCommand = new CommandDefinition(
                @"delete from TaskTaskConflict
                   where FirstTaskId = @taskId
                      or SecondTaskId = @taskId",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            _ = await Connection.ExecuteAsync(deleteCommand);

            var insertCommand = new CommandDefinition(
                @"insert into TaskTaskConflict
                         (FirstTaskId, SecondTaskId)
                  values (@taskId, @conflictedId)",
                conflictedIds.Select(t => new { conflictedId = t, taskId }),
                Transaction,
                flags: CommandFlags.NoCache);

            _ = await Connection.ExecuteAsync(insertCommand);
        }

        public async Task RemoveTaskConflictAsync(long firstTaskId, long secondTaskId)
        {
            var command = new CommandDefinition(
                @"delete from TaskTaskConflict
                   where ( FirstTaskId = @firstTaskId and SecondTaskId = @secondTaskId )
                      or ( FirstTaskId = @secondTaskId and SecondTaskId = @firstTaskId )",
                new { firstTaskId, secondTaskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            _ = await Connection.ExecuteAsync(command);
        }

        public async Task RemoveTaskConflictsByTaskIdAsync(long taskId)
        {
            var command = new CommandDefinition(
                @"delete from TaskTaskConflict
                   where FirstTaskId = @taskId
                      or SecondTaskId = @taskId",
                new { taskId },
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            _ = await Connection.ExecuteAsync(command);
        }

        public async Task<IEnumerable<(long FirstTaskId, long SecondTaskId)>> QueryTaskConflictsAsync()
        {
            var command = new CommandDefinition(
                @"select FirstTaskId, SecondTaskId
                    from TaskTaskConflict",
                transaction: Transaction,
                flags: CommandFlags.NoCache);

            return await Connection.QueryAsync<(long FirstTaskId, long SecondTaskId)>(command);
        }

    }
}
