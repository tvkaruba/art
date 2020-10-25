using Art.Persistence.Entities;
using Art.Web.Shared.Models.Variant;
using AutoMapper;

namespace Art.Web.Server.Mappings
{
    public class VariantProfile : Profile
    {
        public VariantProfile()
        {
            CreateMap<Variant, VariantGet>(MemberList.Destination);

            CreateMap<VariantPost, Variant>(MemberList.Destination);

            CreateMap<VariantPut, Variant>(MemberList.Destination);

            CreateMap<VariantChangeLog, VariantHistoryGet>(MemberList.Destination);
        }
    }
}
