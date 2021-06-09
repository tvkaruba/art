using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("QuestionImageLink")]
    public class QuestionImageLink : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long ImageId { get; set; }

        public long QuestionId { get; set; }
    }
}