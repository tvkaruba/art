using Art.Persistence.ReferenceData;
using Art.Web.Shared.Models.Common;

namespace Art.Web.Shared.Models.Task
{
    public class TaskFilters
    {
        public string SearchTerm { get; set; }

        public Module? ModuleId { get; set; }

        public TaskType? TaskTypeId { get; set; }

        public SortType? SortTypeId { get; set; }
    }
}
