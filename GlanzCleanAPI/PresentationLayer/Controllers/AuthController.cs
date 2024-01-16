using BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs;
using BusinessManagementAPI.Utilities.ErrorHandling.BadRequestExceptions.AuthBadRequestExceptions;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BusinessManagementAPI.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager) => _serviceManager = serviceManager;
        // GET: api/<AuthController>
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null) throw new AuthInvalidDataBadRequest("Register data is null.");

            var result = await _serviceManager.AuthService.RegisterUser(registerDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (loginDto == null) throw new AuthInvalidDataBadRequest("Login data is null.");

            
            if (!await _serviceManager.AuthService.ValidateUser(loginDto)) return Unauthorized();

            return Ok(new { Token = await _serviceManager.AuthService.CreateToken(loginDto) });
        }

        [HttpPost("changePass")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePass([FromBody] ChangePassDto changePassDto)
        {
            if (changePassDto == null) throw new AuthInvalidDataBadRequest("Email is null");

            var result = await _serviceManager.AuthService.ChangePassword(changePassDto);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201);
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Test WORKS!");
        }

    }
}
