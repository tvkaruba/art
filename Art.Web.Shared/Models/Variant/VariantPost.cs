using System.Collections.Generic;
using Art.Persistence.ReferenceData;

namespace Art.Web.Shared.Models.Variant
{
    public class VariantPost
    {
        public Module ModuleId { get; set; }

        public string Name { get; set; }

        public IEnumerable<long> TaskIds { get; set; }
    }
}
