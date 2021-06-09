using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("TestTagLink")]
    public class TestTagLink : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long TestId { get; set; }

        public long TagId { get; set; }
    }
}