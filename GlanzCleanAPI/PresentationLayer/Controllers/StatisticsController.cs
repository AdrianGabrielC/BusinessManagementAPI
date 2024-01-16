using BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessManagementAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public StatisticsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }
        // GET: api/<StatisticsController>
        [HttpGet("yearStats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetYearStats([FromQuery]GetWorkYearRevRequestDto parameters)
        {
            var statistics = await _serviceManager.StatisticsService.GetYearStatsAsync(parameters.Year);
            return Ok(statistics);
        }

        [HttpGet("allYearsStats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllYearsStats()
        {
            var statistics = await _serviceManager.StatisticsService.GetAllYearsStatsAsync();
            return Ok(statistics);
        }

        [HttpGet("employeeYearRevStats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeYearRevenueStats([FromQuery] GetEmployeeYearRevStatsRequestDto reqDto)
        {
            var statistics = await _serviceManager.StatisticsService.GetEmployeeStatsYearRevenueAsync(reqDto.Year, reqDto.EmployeeId);
            return Ok(statistics);
        }

        [HttpGet("employeeYearHoursWorkedStats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEmployeeYearHoursWorkedStats([FromQuery] GetEmployeeYearRevStatsRequestDto reqDto)
        {
            var statistics = await _serviceManager.StatisticsService.GetEmployeeStatsYearHoursWorkedAsync(reqDto.Year, reqDto.EmployeeId);
            return Ok(statistics);
        }
    }
}
