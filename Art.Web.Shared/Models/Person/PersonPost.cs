using Art.Persistence.ReferenceData;

namespace Art.Web.Shared.Models.Person
{
    public class PersonPost
    {
        public PersonRole PersonRoleId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }
    }
}