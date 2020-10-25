using Art.Persistence.ReferenceData;
using System;

namespace Art.Web.Shared.Models.Person
{
    public class PersonGet
    {
        public long Id { get; set; }

        public PersonRole PersonRoleId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime ChangedAtUtc { get; set; }
    }
}