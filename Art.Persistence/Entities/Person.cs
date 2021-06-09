using System;
using Art.Persistence.Infrastructure.Abstractions;
using Art.Persistence.ReferenceData;
using Dapper.Contrib.Extensions;

namespace Art.Persistence.Entities
{
    [Table("Person")]
    public class Person : IEntity<long>, IUtcAuditableEntity
    {
        [Key]
        public long Id { get; set; }

        public PersonRole PersonRoleId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Computed]
        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ChangedAtUtc { get; set; }
    }
}
