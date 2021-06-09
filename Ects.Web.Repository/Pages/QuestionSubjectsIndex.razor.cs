using System.Collections.Generic;
using System.Threading.Tasks;
using Ects.Persistence.Models;
using Ects.Persistence.Repositories;
using Microsoft.AspNetCore.Components;

namespace Ects.Web.Repository.Pages
{
    public abstract class SubjectsIndexBase : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public async Task<List<Namespace>> GetSubjects()
        {
            return await Task.FromResult(FakeRepository.Namespaces);
        }

        public void ViewSubject(long id)
        {
            NavigationManager.NavigateTo($"questions/namespaces/{id}");
        }

        public void AddSubject()
        {
            NavigationManager.NavigateTo("namespace-new");
        }
    }
}