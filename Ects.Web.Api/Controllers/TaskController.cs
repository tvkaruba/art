using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Persistence.ReferenceData;
using Art.Web.Server.Filters;
using Art.Web.Server.Services.Abstractions;
using Art.Web.Shared.Models.Common;
using Art.Web.Shared.Models.Errors;
using Art.Web.Shared.Models.Task;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Art.Web.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(RolePolicies.User)]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService ?? throw new ArgumentException(nameof(taskService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<TaskGet>))]

        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("count")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(long))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Filters were invalid format", typeof(IEnumerable<ValidationError>))]

        public async Task<IActionResult> GetCount(
            Module? module = null,
            TaskType? type = null,
            string searchTerm = null)
        {
            var result = await _taskService.GetTasksCountWithFiltersAsync(
                new TaskFilters
                {
                    ModuleId = module,
                    TaskTypeId = type,
                    SortTypeId = null,
                    SearchTerm = searchTerm,
                });
            return Ok(result);
        }

        [HttpGet]
        [Route("paged/{page:int}/{pageSize:int}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(long))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Filters were invalid format", typeof(IEnumerable<ValidationError>))]

        public async Task<IActionResult> GetPaged(
            int page,
            int pageSize,
            Module? module = null,
            TaskType? type = null,
            SortType? sort = null,
            string searchTerm = null)
        {
            var result = await _taskService.GetTasksWithPaginationAndFiltersAsync(
                page: page,
                itemsOnPage: pageSize,
                filters: new  TaskFilters
                {
                    ModuleId = module,
                    TaskTypeId = type,
                    SortTypeId = sort,
                    SearchTerm = searchTerm,
                });
            return Ok(result);
        }

        [HttpGet]
        [Route("history/{id:long}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<TaskHistoryGet>))]

        public async Task<IActionResult> GetTaskHistory(long id)
        {
            var result = await _taskService.GetTaskHistoriesByTaskIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:long}", Name = nameof(GetTask))]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(TaskGet))]
        public async Task<IActionResult> GetTask(long id)
        {
            var person = await _taskService.GetAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status201Created, "Created task id.", typeof(long))]
        public async Task<IActionResult> Create([FromBody] TaskPost data)
        {
            var task = await _taskService.CreateAsync(data);
            return CreatedAtRoute(nameof(GetTask), new { id = task.Id }, task.Id);
        }

        [HttpPut]
        [Route("{id:long}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, type: typeof(void))]
        public async Task<IActionResult> Update(long id, [FromBody] TaskPut data)
        {
            var task = await _taskService.UpdateAsync(id, data);

            if (task == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id:long}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, type: typeof(void))]
        public async Task<IActionResult> Delete(long id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("topic")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<TaskTopicsGet>))]
        public async Task<IActionResult> GetAllTopics()
        {
            var result = await _taskService.GetAllTopicsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("tag")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<TaskTagsGet>))]
        public async Task<IActionResult> GetAllTags()
        {
            var result = await _taskService.GetAllTagsAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("conflict")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<TaskConflictsGet>))]
        public async Task<IActionResult> GetAllConflicts()
        {
            var result = await _taskService.GetTaskConflictsAsync();
            return Ok(result);
        }

        [HttpDelete]
        [Route("conflict/{first:long}/{second:long}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, type: typeof(void))]
        public async Task<IActionResult> DeleteConflict(long first, long second)
        {
            await _taskService.RemoveTaskConflictsAsync(first, second);
            return NoContent();
        }
    }
}
