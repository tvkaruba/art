using System;
using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("ExamParticipant")]
    public class ExamParticipant : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long ExamId { get; set; }

        public long AccountId { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? Result { get; set; }

        public double? MaxResult { get; set; }
    }
}