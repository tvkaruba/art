using System.Collections.Generic;
using Art.Persistence.ReferenceData;

namespace Art.Web.Shared.Models.Task
{
    public class TaskPost
    {
        public Module ModuleId { get; set; }

        public IEnumerable<string> Topics { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public TaskType TaskTypeId { get; set; }

        public IEnumerable<string> Answers { get; set; }

        public IEnumerable<string> Rights { get; set; }
    }
}
