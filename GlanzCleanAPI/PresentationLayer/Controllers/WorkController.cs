using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlanzCleanAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public WorkController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/Work
        [HttpGet]
        public async Task<ActionResult> GetWork([FromQuery] WorkParameters workParameters)
        {
            var pagedResult = await _serviceManager.WorkService.GetWorkAsync<WorkDto>(workParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.work);

        }

        // GET api/<WorkController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetWork(Guid id)
        {
            var work = await _serviceManager.WorkService.GetWorkByIdAsync<WorkDto>(id, false);

            return Ok(work);
        }

        // POST api/<WorkController>
        [HttpPost]
        public async Task<ActionResult> PostWork([FromBody] WorkPostDto work)
        {
            if (work is null) return BadRequest("Work object is null");
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var createdWork = await _serviceManager.WorkService.CreateWorkAsync(work);

            return CreatedAtAction("GetWork", new { id = createdWork.Id }, createdWork);

        }

        // PUT api/<WorkController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWork(Guid id, [FromBody] WorkPutDto work)
        {
            if (work is null) return BadRequest("Work parameter is null");

            await _serviceManager.WorkService.UpdateWorkAsync(id, work, trackChanges: true);

            return NoContent();
        }

        //DELETE api/<WorkController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWork(Guid id)
        {
            await _serviceManager.WorkService.DeleteWorkAsync(id);

            return NoContent();
        }
    }
}
