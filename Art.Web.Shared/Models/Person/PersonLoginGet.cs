namespace Art.Web.Shared.Models.Person
{
    public class PersonLoginGet
    {
        public string JwtToken { get; set; }

        public string Role { get; set; }

        public string Email { get; set; }

        public long Id { get; set; }
    }
}
