using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;

namespace GlanzCleanAPI.Utilities.Profiles
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile() { 
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
            CreateMap<EmployeePutDto, Employee>();
            CreateMap<EmployeePostDto, Employee>();
        }
    }
}
