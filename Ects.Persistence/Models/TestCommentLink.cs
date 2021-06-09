using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("TestCommentLink")]
    public class TestCommentLink : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long TestId { get; set; }

        public long CommentId { get; set; }
    }
}