using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("QuestionCommentLink")]
    public class QuestionCommentLink : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long CommentId { get; set; }

        public long QuestionId { get; set; }
    }
}