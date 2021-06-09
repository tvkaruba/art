using System.Collections.Generic;
using System.Threading.Tasks;
using Ects.Web.Shared.Models.QuestionType;
using Microsoft.AspNetCore.Components;

namespace Ects.Web.Repository.Pages
{
    public abstract class QuestionNewBase : ComponentBase
    {
        public IEnumerable<QuestionTypeRestrictedGet> QuestionTypes { get; set; }

        public async Task ViewQuestionType(long id) { }

        public async Task AddQuestionType() { }
    }
}