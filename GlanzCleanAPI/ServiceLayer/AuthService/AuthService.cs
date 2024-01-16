using AutoMapper;
using BusinessManagementAPI.CoreLayer.Entities;
using BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs;
using BusinessManagementAPI.Utilities.ErrorHandling.NotFoundExceptions;
using BusinessManagementAPI.Utilities.ErrorHandling.UnauthorizedExceptions;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessManagementAPI.ServiceLayer.AuthService
{
    internal sealed class AuthService:IAuthService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repository;

        public AuthService(IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IRepositoryManager repositoryManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repositoryManager;
        }

        // REGISTER 
        public async Task<IdentityResult> RegisterUser(RegisterDto registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            user.UserName = registerDto.Email;
            user.EmployeeId = Guid.NewGuid();
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                // Add roles
                await _userManager.AddToRolesAsync(user, registerDto.Roles);

                // Add employee
                var employee = _mapper.Map<Employee>(registerDto);
                employee.Id = user.EmployeeId;
                _repository.Employees.CreateEmployee(employee);
                await _repository.SaveAsync();
            }
            return result;
        }

        // LOGIN
        public async Task<bool> ValidateUser(LoginDto loginDto)
        {
            var _user = await _userManager.FindByNameAsync(loginDto.UserName);
            var employee = await _repository.Employees.GetEmployeeByEmailAsync(loginDto.UserName, false);
            if (employee == null || !employee.IsActive) throw new UserNotFoundException();

            var isValid = _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
            return isValid;
        }

        public async Task<string> CreateToken(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public async Task<IdentityResult> ChangePassword(ChangePassDto changePassDto)
        {
            var user = await _userManager.FindByNameAsync(changePassDto.UserName);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, changePassDto.NewPassword);
            return result;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes("Kp07LCF9ghCJ7+K5i5QDhFhYzM9+6AHUT3QU/7b4fFg=");
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User u)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, u.UserName)
            };
            var roles = await _userManager.GetRolesAsync(u);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var tokenOptions = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                    signingCredentials: signingCredentials
                    );
            return tokenOptions;
        }
    }
}
