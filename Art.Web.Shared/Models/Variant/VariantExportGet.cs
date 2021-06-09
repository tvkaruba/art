using System.Collections.Generic;
using Art.Persistence.ReferenceData;
using Art.Web.Shared.Models.Task;

namespace Art.Web.Shared.Models.Variant
{
    public class VariantExportGet
    {
        public long Id { get; set; }

        public Module ModuleId { get; set; }

        public string Name { get; set; }

        public IEnumerable<TaskGet> Tasks { get; set; }
    }
}
