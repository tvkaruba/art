using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("QuestionQuestionConflict")]
    public class QuestionQuestionConflict : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long FirstQuestionId { get; set; }

        public long SecondQuestionId { get; set; }

        public bool IsResolved { get; set; }
    }
}