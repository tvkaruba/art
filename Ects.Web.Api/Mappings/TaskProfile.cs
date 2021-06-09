using AutoMapper;
using Ects.Persistence.Models;
using Ects.Web.Shared.Models.Task;

namespace Ects.Web.Api.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Question, TaskGet>(MemberList.Destination);

            CreateMap<TaskPost, Question>(MemberList.Destination);

            CreateMap<TaskPut, Question>(MemberList.Destination);
        }
    }
}