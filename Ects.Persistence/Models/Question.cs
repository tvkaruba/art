using System;
using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("Question")]
    public class Question : IEntity<long>, IAuditable
    {
        [Key]
        public long Id { get; set; }

        public long NamespaceId { get; set; }

        public long QuestionTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public string Answers { get; set; }

        public string Rights { get; set; }

        public long Likes { get; set; }

        public long Dislikes { get; set; }

        public bool IsActive { get; set; }

        [Computed]
        public DateTime CreatedAtUtc { get; set; }

        public long CreatedBy { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public long? ChangedBy { get; set; }
    }
}