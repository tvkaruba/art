using Art.Persistence.Entities;
using Art.Web.Shared.Models.Person;
using AutoMapper;

namespace Art.Web.Server.Mappings
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Person, PersonGet>(MemberList.Destination);

            CreateMap<PersonPost, Person>(MemberList.Destination);

            CreateMap<PersonPut, Person>(MemberList.Destination)
                .ForMember(dst => dst.PersonRoleId, opt => opt.Condition(src => src.PersonRoleId.HasValue))
                .ForMember(dst => dst.Email, opt => opt.Condition(src => src.Email != null))
                .ForMember(dst => dst.Password, opt => opt.Condition(src => src.Password != null))
                .ForMember(dst => dst.FirstName, opt => opt.Condition(src => src.FirstName != null))
                .ForMember(dst => dst.SecondName, opt => opt.Condition(src => src.SecondName != null));
        }
    }
}