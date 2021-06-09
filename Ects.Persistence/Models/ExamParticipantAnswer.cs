using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("ExamParticipantAnswer")]
    public class ExamParticipantAnswer : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long ExamParticipantId { get; set; }

        public long QuestionId { get; set; }

        public string Answer { get; set; }

        public double Value { get; set; }
    }
}