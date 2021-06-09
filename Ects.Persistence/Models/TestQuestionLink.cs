using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("TestQuestionLink")]
    public class TestQuestionLink : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long TestId { get; set; }

        public long QuestionId { get; set; }

        public double Value { get; set; }

        public long Order { get; set; }
    }
}