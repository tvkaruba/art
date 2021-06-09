using System;
using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("QuestionHistory")]
    public class QuestionHistory : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long QuestionId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Body { get; set; }

        public string Answers { get; set; }

        public string Rights { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public long? ChangedBy { get; set; }
    }
}