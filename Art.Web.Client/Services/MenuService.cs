using System;
using Art.Web.Client.Services.Abstractions;

namespace Art.Web.Client.Services
{
    public class MenuService : IMenuService
    {
        public bool HasConflicts { get; set; }

        public event EventHandler<EventArgs> OnChanged;

        public void NotifyChanged()
        {
            OnChanged?.Invoke(HasConflicts, new EventArgs());
        }
    }
}
