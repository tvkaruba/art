using System.Threading.Tasks;
using Art.Web.Shared.Models.Task;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace Art.Web.Client.Pages.Components
{
    public class TaskCardBase : ComponentBase
    {
        [Parameter] public EventCallback<long> OnEditTaskCallback { get; set; }

        [Parameter] public EventCallback<long> OnViewHistoryCallback { get; set; }

        [Parameter] public EventCallback<long> OnRemoveTaskCallback { get; set; }

        [Parameter] public TaskGet Task { get; set; } = null;

        public string RenderedTaskBody =>
            Task == null
                ? string.Empty
                : Markdown.ToHtml(Task.Body, _pipeline);

        private readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder().UseSyntaxHighlighting().Build();

        public async Task EditTask() => await OnEditTaskCallback.InvokeAsync(Task.Id);

        public async Task ViewHistory() => await OnViewHistoryCallback.InvokeAsync(Task.Id);

        public async Task RemoveTask() => await OnRemoveTaskCallback.InvokeAsync(Task.Id);
    }
}
