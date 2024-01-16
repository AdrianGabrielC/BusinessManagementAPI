using AutoMapper;
using BusinessManagementAPI.CoreLayer.Entities;
using BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs;

namespace BusinessManagementAPI.Utilities.Profiles
{
    public class AuthProfile: Profile
    {
        public AuthProfile() 
        {
            CreateMap<RegisterDto, User>();
        }
    }
}
