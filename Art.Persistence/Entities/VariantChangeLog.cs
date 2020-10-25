using System;
using Art.Persistence.Infrastructure.Abstractions;
using Dapper.Contrib.Extensions;

namespace Art.Persistence.Entities
{
    [Table("VariantChangeLog")]
    public class VariantChangeLog : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public long VariantId { get; set; }

        public string Record { get; set; }

        [Computed]
        public DateTime CreatedAtUtc { get; set; }
    }
}
