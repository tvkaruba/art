using Ects.Web.Api.Validators.Infrastructure;
using Ects.Web.Shared.Models.Task;
using FluentValidation;

namespace Ects.Web.Api.Validators.Task
{
    public class TaskPutValidationRules : ValidationRulesBase<TaskPut>
    {
        public TaskPutValidationRules()
        {
            RuleFor(data => data)
                .NotNull();

            RuleFor(data => data.Name)
                .NotNull()
                .NotEmpty();

            RuleFor(data => data.Body)
                .NotNull()
                .NotEmpty();

            RuleFor(data => data.ModuleId)
                .IsInEnum();

            RuleFor(data => data.TaskTypeId)
                .IsInEnum();

            RuleFor(data => data.Answers)
                .NotNull();

            RuleFor(data => data.Rights)
                .NotNull();

            RuleFor(data => data.Topics)
                .NotNull();

            RuleFor(data => data.Tags)
                .NotNull();
        }
    }
}