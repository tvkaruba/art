using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("Tag")]
    public class Tag : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}