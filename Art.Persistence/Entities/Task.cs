using System;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Persistence.ReferenceData;
using Dapper.Contrib.Extensions;

namespace Art.Persistence.Entities
{
    [Table("Task")]
    public class Task : IEntity<long>, IUtcAuditableEntity
    {
        [Key]
        public long Id { get; set; }

        public Module ModuleId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public TaskType TaskTypeId { get; set; }

        [Computed]
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public bool IsActive { get; set; }
    }
}
