using System;

namespace Art.Web.Client.Services.Abstractions
{
    public interface IMenuService
    {
        bool HasConflicts { get; set; }

        event EventHandler<EventArgs> OnChanged;

        void NotifyChanged();
    }
}
