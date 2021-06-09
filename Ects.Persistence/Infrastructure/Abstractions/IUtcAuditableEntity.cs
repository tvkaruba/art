using System;

namespace Art.Persistence.Infrastructure.Abstractions
{
    public interface IUtcAuditableEntity
    {
        DateTime? ChangedAtUtc { get; set; }
    }
}
