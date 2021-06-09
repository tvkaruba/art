using System;
using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("Comment")]
    public class Comment : IEntity<long>, IAuditable
    {
        [Key]
        public long Id { get; set; }

        public string Body { get; set; }

        public bool IsActive { get; set; }

        [Computed]
        public DateTime CreatedAtUtc { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public long? ChangedBy { get; set; }
    }
}