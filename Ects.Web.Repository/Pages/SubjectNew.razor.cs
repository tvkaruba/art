using Ects.Persistence.Models;
using Ects.Persistence.Repositories;
using Microsoft.AspNetCore.Components;

namespace Ects.Web.Repository.Pages
{
    public abstract class SubjectNewBase : ComponentBase
    {
        protected Namespace Subject { get; set; }

        protected Image Image { get; set; }

        protected string ErrorMessage { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override void OnInitialized()
        {
            Subject = new Namespace();
            Image = new Image();
        }

        public void AddSubject()
        {
            Subject.Id = FakeRepository.Namespaces.Count;
            Subject.ImageId = FakeRepository.Images.Count;
            Image.Id = FakeRepository.Images.Count;
            FakeRepository.Namespaces.Add(Subject);
            FakeRepository.Images.Add(Image);
            NavigationManager.NavigateTo("");
        }
    }
}