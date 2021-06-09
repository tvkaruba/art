using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("QuestionTagLink")]
    public class QuestionTagLink : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long TagId { get; set; }

        public long QuestionId { get; set; }
    }
}