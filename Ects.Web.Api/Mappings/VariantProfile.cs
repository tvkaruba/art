using AutoMapper;
using Ects.Persistence.Models;
using Ects.Web.Shared.Models.Variant;

namespace Ects.Web.Api.Mappings
{
    public class VariantProfile : Profile
    {
        public VariantProfile()
        {
            CreateMap<Test, VariantGet>(MemberList.Destination);

            CreateMap<VariantPost, Test>(MemberList.Destination);

            CreateMap<VariantPut, Test>(MemberList.Destination);
        }
    }
}