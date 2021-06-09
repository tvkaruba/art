using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph;

namespace Ects.Web.Repository.Pages.Components
{
    public abstract class UserProfileBase : ComponentBase
    {
        protected User User = new();

        [Inject]
        public IAuthenticationProvider AuthProvider { get; set; }

        [Inject]
        public IHttpProvider Http { get; set; }

        private GraphServiceClient GraphClient { get; set; }

        protected override async Task OnInitializedAsync()
        {
            GraphClient = new GraphServiceClient(AuthProvider, Http);
            await GetUserProfile();
        }

        private async Task GetUserProfile()
        {
            var request = GraphClient.Me.Request();
            User = await request.GetAsync();
        }
    }
}