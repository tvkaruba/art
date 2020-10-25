using System.Threading.Tasks;
using Art.Web.Shared.Models.Task;
using Markdig;
using Microsoft.AspNetCore.Components;

namespace Art.Web.Client.Pages.Components
{
    public class ConflictCardBase : ComponentBase
    {
        [Parameter] public EventCallback<long> OnKeepLeftTaskCallback { get; set; }

        [Parameter] public EventCallback<long> OnKeepRightTaskCallback { get; set; }

        [Parameter] public EventCallback<(long left, long right)> OnKeepBothTasksCallback { get; set; }

        [Parameter] public TaskGet LeftTask { get; set; } = null;

        [Parameter] public TaskGet RightTask { get; set; } = null;

        public string RenderedLeftTaskBody =>
            LeftTask == null
                ? string.Empty
                : Markdown.ToHtml(LeftTask.Body, _pipeline);

        public string RenderedRightTaskBody =>
            RightTask == null
                ? string.Empty
                : Markdown.ToHtml(RightTask.Body, _pipeline);

        private readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder().UseSyntaxHighlighting().Build();

        public async Task KeepLeft() => await OnKeepLeftTaskCallback.InvokeAsync(RightTask.Id);

        public async Task KeepRight() => await OnKeepRightTaskCallback.InvokeAsync(LeftTask.Id);

        public async Task KeepBoth() => await OnKeepBothTasksCallback.InvokeAsync((left: LeftTask.Id, right: RightTask.Id));
    }
}
