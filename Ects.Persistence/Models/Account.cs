using System;
using Dapper.Contrib.Extensions;
using Ects.Persistence.Models.Abstractions;

namespace Ects.Persistence.Models
{
    [Table("Account")]
    public class Account : IEntity<long>
    {
        [Key]
        public long Id { get; set; }

        public Guid Oid { get; set; }

        public string Login { get; set; }

        public string Name { get; set; }

        public string Roles { get; set; }

        public string Groups { get; set; }

        public string StudyGroup { get; set; }

        public bool IsActive { get; set; }
    }
}