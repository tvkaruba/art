using AutoMapper;
using Ects.Persistence.Models;
using Ects.Web.Shared.Models.Person;

namespace Ects.Web.Api.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Account, PersonGet>(MemberList.Destination);

            CreateMap<PersonPost, Account>(MemberList.Destination);

            CreateMap<PersonPut, Account>(MemberList.Destination);
        }
    }
}