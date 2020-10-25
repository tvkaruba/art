using System.ComponentModel.DataAnnotations;
using Art.Persistence.ReferenceData;

namespace Art.Web.Client.Models
{
    public class RegistrationForm
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string SecondName { get; set; }

        [Required]
        [EnumDataType(typeof(PersonRole))]
        public PersonRole Role { get; set; }
    }
}
