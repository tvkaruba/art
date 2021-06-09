using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("Image")]
    public class Image : IEntity<long>
    {
        [Computed]
        public long Id { get; set; }

        public string Url { get; set; }
    }
}