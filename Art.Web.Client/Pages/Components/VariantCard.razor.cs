using System.Threading.Tasks;
using Art.Web.Shared.Models.Variant;
using Microsoft.AspNetCore.Components;

namespace Art.Web.Client.Pages.Components
{
    public class VariantCardBase : ComponentBase
    {
        [Parameter] public EventCallback<long> OnEditCallback { get; set; }

        [Parameter] public EventCallback<long> OnViewHistoryCallback { get; set; }

        [Parameter] public EventCallback<long> OnRemoveCallback { get; set; }

        [Parameter] public EventCallback<long> OnExportCallback { get; set; }

        [Parameter] public VariantGet Variant { get; set; } = null;

        public async Task EditVariant() => await OnEditCallback.InvokeAsync(Variant.Id);

        public async Task ViewHistory() => await OnViewHistoryCallback.InvokeAsync(Variant.Id);

        public async Task ExportVariant() => await OnExportCallback.InvokeAsync(Variant.Id);

        public async Task RemoveVariant() => await OnRemoveCallback.InvokeAsync(Variant.Id);
    }
}