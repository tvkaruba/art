using Art.Web.Server.Validators.Infrastructure;
using Art.Web.Shared.Models.Task;
using FluentValidation;

namespace Art.Web.Server.Validators.Task
{
    /// <inheritdoc />
    public class TaskFiltersValidationRules : ValidationRulesBase<TaskFilters>
    {
        public TaskFiltersValidationRules()
        {
            RuleFor(filters => filters.ModuleId)
                .IsInEnum()
                .When(filters => filters.ModuleId.HasValue);

            RuleFor(filters => filters.TaskTypeId)
                .IsInEnum()
                .When(filters => filters.TaskTypeId.HasValue);

            RuleFor(filters => filters.SortTypeId)
                .IsInEnum()
                .When(filters => filters.SortTypeId.HasValue);
        }
    }
}
