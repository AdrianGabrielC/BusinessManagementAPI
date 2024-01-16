using AutoMapper;
using BusinessManagementAPI.CoreLayer.Entities;
using BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs;
using Microsoft.AspNetCore.Identity;

namespace BusinessManagementAPI.ServiceLayer.AuthService
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(RegisterDto registerDto);
        Task<bool> ValidateUser(LoginDto loginDto);
        Task<string> CreateToken(LoginDto loginDto);
        Task<IdentityResult> ChangePassword(ChangePassDto changePassDto);
    }
}
