using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("QuestionType")]
    public class QuestionType : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        [Computed]
        public string Type { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}