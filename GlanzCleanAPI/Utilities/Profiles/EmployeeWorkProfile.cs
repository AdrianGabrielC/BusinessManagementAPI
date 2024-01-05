using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs;

namespace GlanzCleanAPI.Utilities.Profiles
{
    public class EmployeeWorkProfile: Profile
    {
        public EmployeeWorkProfile() {
            CreateMap<EmployeeWork, EmployeeWorkDto>();
            CreateMap<EmployeeWorkDto, EmployeeWork>();
            CreateMap<EmployeeWorkPostDto, EmployeeWork>();
            CreateMap<EmployeeWorkPutDto, EmployeeWork>();
        }
    }
}
