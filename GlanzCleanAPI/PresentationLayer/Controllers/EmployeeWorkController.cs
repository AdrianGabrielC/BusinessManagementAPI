using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlanzCleanAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeWorkController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeeWorkController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<EmployeeWorkController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetEmployeeWork([FromQuery] EmployeeWorkParameters employeeWorkParameters)
        {
            var pagedResult = await _serviceManager.EmployeeWorkService.GetEmployeeWorkAsync<EmployeeWorkDto>(employeeWorkParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.employeeWork);

        }

        // GET api/<EmployeeWorkController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetEmployeeWork(Guid id)
        {
            var employeeWork = await _serviceManager.EmployeeWorkService.GetEmployeeWorkByIdAsync<EmployeeWorkDto>(id, false);

            return Ok(employeeWork);
        }

        // POST api/<EmployeeWorkController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostEmployeeWork([FromBody] EmployeeWorkDto employeeWork)
        {
            if (employeeWork is null) return BadRequest("Employee work object is null");
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var createdEmployee = await _serviceManager.EmployeeWorkService.CreateEmployeeWorkAsync(employeeWork);

            return CreatedAtAction("GetEmployeeWork", new { id = createdEmployee.Id }, createdEmployee);

        }

        // PUT api/<EmployeeWorkController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutEmployeeWork(Guid id, [FromBody] EmployeeWorkPutDto employeeWork)
        {
            if (employeeWork is null) return BadRequest("Employee work parameter is null");

            await _serviceManager.EmployeeWorkService.UpdateEmployeeWorkAsync(id, employeeWork, trackChanges: true);

            return NoContent();
        }

        // DELETE api/<EmployeeWorkController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteEmployeeWork(Guid id)
        {
            await _serviceManager.EmployeeWorkService.DeleteEmployeeWorkAsync(id);

            return NoContent();
        }
    }
}
