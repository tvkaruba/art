using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Art.Web.Server.Filters;
using Art.Web.Server.Services.Abstractions;
using Art.Web.Shared.Models.Task;
using Art.Web.Shared.Models.Variant;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Art.Web.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(RolePolicies.User)]
    public class VariantController : ControllerBase
    {
        private readonly IVariantService _variantService;

        public VariantController(IVariantService variantService)
        {
            _variantService = variantService ?? throw new ArgumentException(nameof(variantService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<VariantGet>))]

        public async Task<IActionResult> GetAll()
        {
            var result = await _variantService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet]
        [Route("history/{id:long}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<VariantHistoryGet>))]

        public async Task<IActionResult> GetTaskHistory(long id)
        {
            var result = await _variantService.GetVariantHistoryAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:long}", Name = nameof(GetVariant))]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(VariantGet))]
        public async Task<IActionResult> GetVariant(long id)
        {
            var person = await _variantService.GetAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [Route("")]
        [SwaggerResponse(StatusCodes.Status201Created, "Created task id.", typeof(long))]
        public async Task<IActionResult> Create([FromBody] VariantPost data)
        {
            var task = await _variantService.CreateAsync(data);
            return CreatedAtRoute(nameof(GetVariant), new { id = task.Id }, task.Id);
        }

        [HttpPut]
        [Route("{id:long}")]
        [SwaggerResponse(StatusCodes.Status204NoContent, type: typeof(void))]
        public async Task<IActionResult> Update(long id, [FromBody] VariantPut data)
        {
            var task = await _variantService.UpdateAsync(id, data);

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
            await _variantService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("{id:long}/tasks")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<TaskGet>))]
        public async Task<IActionResult> GetVariantTasks(long id)
        {
            var result = await _variantService.GetTasksByVariantIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("export/{id:long}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(VariantExportGet))]
        public async Task<IActionResult> GetVariantForExport(long id)
        {
            var result = await _variantService.GetVariantForExportByVariantId(id);
            return Ok(result);
        }
    }
}
