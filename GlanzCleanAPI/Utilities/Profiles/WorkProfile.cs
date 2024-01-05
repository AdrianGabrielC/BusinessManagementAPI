using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs;

namespace GlanzCleanAPI.Utilities.Profiles
{
    public class WorkProfile: Profile
    {
        public WorkProfile() {
            CreateMap<Work, WorkDto>();
            CreateMap<WorkDto, Work>();
            CreateMap<WorkPutDto, Work>();
            CreateMap<WorkPostDto, Work>();
        }
    }
}
