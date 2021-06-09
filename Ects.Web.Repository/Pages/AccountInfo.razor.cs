using Microsoft.AspNetCore.Components;

namespace Ects.Web.Repository.Pages
{
    public abstract class AccountInfoBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public void SeeResults()
        {
            NavigationManager.NavigateTo("exam-results");
        }
    }
}