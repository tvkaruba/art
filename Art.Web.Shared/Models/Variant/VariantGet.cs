using System;
using Art.Persistence.ReferenceData;

namespace Art.Web.Shared.Models.Variant
{
    public class VariantGet
    {
        public long Id { get; set; }

        public Module ModuleId { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? ChangedAtUtc { get; set; }

        public bool IsActive { get; set; }
    }
}
