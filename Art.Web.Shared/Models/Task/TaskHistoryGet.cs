using System;
using Art.Persistence.ReferenceData;

namespace Art.Web.Shared.Models.Task
{
    public class TaskHistoryGet
    {
        public long Id { get; set; }

        public Module ModuleId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public TaskType TaskTypeId { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public bool IsActive { get; set; }
    }
}
