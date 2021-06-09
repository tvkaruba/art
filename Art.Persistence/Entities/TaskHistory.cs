using System;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Persistence.ReferenceData;
using Dapper.Contrib.Extensions;

namespace Art.Persistence.Entities
{
    [Table("TaskHistory")]
    public class TaskHistory : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public Module ModuleId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public TaskType TaskTypeId { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public bool IsActive { get; set; }
    }
}
