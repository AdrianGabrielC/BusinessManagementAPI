using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GlanzCleanAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public EmployeesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult> GetEmployees([FromQuery] EmployeeParameters employeeParameters)
        {
            var pagedResult = await _serviceManager.EmployeeService.GetEmployeesAsync<EmployeeDto>(employeeParameters, false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));

            return Ok(pagedResult.employees);

        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEmployee(Guid id)
        {
            var employee = await _serviceManager.EmployeeService.GetEmployeeByIdAsync<EmployeeDto>(id, false);

            return Ok(employee);
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> PostEmployee([FromBody]EmployeeDto employee)
        {
            if (employee is null) return BadRequest("Employee object is null");
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);

            var createdEmployee = await _serviceManager.EmployeeService.CreateEmployeeAsync(employee);

            return CreatedAtAction("GetEmployee", new { id = createdEmployee.Id }, createdEmployee);

        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(Guid id, [FromBody]EmployeePutDto employee)
        {
            if (employee is null) return BadRequest("Employee parameter is null");

            await _serviceManager.EmployeeService.UpdateEmployeeAsync(id, employee, trackChanges: true);

            return NoContent();
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            await _serviceManager.EmployeeService.DeleteEmployeeAsync(id);

            return NoContent();
        }


    }
}
