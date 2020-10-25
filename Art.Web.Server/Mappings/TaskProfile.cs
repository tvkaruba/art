using Art.Persistence.Entities;
using Art.Web.Shared.Models.Task;
using AutoMapper;

namespace Art.Web.Server.Mappings
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Task, TaskGet>(MemberList.Destination);

            CreateMap<TaskPost, Task>(MemberList.Destination);

            CreateMap<TaskPut, Task>(MemberList.Destination);

            CreateMap<TaskHistory, TaskHistoryGet>(MemberList.Destination);
        }
    }
}
