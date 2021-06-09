using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Art.Web.Client.Pages.Components
{
    public class PagerBase : ComponentBase
    {
        public const int PagerSize = 5;

        [Parameter] public int PageSize { get; set; } = 10;

        [Parameter] public long ItemsCount { get; set; } = 0;

        [Parameter] public EventCallback<int> OnChangePageCallback { get; set; }

        public int CurPage;

        public int StartPage;

        public int EndPage;

        public int PagesCount;

        protected override void OnInitialized()
        {
            CurPage = 1;
            PagesCount = (int)Math.Ceiling(ItemsCount / (decimal)PageSize);
            SetPagerSize("forward");
            StateHasChanged();
        }

        public void SetPagerSize(string direction)
        {
            switch (direction)
            {
                case "forward" when EndPage < PagesCount:
                {
                    StartPage = EndPage + 1;
                    if (EndPage + PagerSize < PagesCount)
                    {
                        EndPage = StartPage + PagerSize - 1;
                    }
                    else
                    {
                        EndPage = PagesCount;
                    }

                    StateHasChanged();
                    break;
                }
                case "back" when StartPage > 1:
                {
                    EndPage = StartPage - 1;
                    StartPage -= PagerSize;

                    StateHasChanged();
                    break;
                }
            }
        }

        public async Task NavigateToPage(int currentPage)
        {
            CurPage = currentPage;
            await OnChangePageCallback.InvokeAsync(CurPage);
            StateHasChanged();
        }

        public async Task NavigateToPage(string direction)
        {
            switch (direction)
            {
                case "next":
                {
                    if (CurPage < PagesCount)
                    {
                        if (CurPage == EndPage)
                        {
                            SetPagerSize("forward");
                        }

                        CurPage += 1;
                    }

                    break;
                }
                case "previous":
                {
                    if (CurPage > 1)
                    {
                        if (CurPage == StartPage)
                        {
                            SetPagerSize("back");
                        }

                        CurPage -= 1;
                    }

                    break;
                }
            }

            await OnChangePageCallback.InvokeAsync(CurPage);
            StateHasChanged();
        }
    }
}
