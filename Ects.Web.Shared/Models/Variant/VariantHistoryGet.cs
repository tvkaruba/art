using System;

namespace Art.Web.Shared.Models.Variant
{
    public class VariantHistoryGet
    {
        public long Id { get; set; }

        public long VariantId { get; set; }

        public string Record { get; set; }

        public DateTime CreatedAtUtc { get; set; }
    }
}
