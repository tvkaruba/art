using System;

namespace Ects.Persistence.Models.Abstractions
{
    public interface IAuditable
    {
        DateTime CreatedAtUtc { get; set; }

        long CreatedBy { get; set; }

        DateTime? ChangedAtUtc { get; set; }

        long? ChangedBy { get; set; }
    }
}