using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs;

namespace GlanzCleanAPI.Utilities.Profiles
{
    public class InvoiceProfile: Profile
    {
        public InvoiceProfile() {
            CreateMap<Invoice, InvoiceDto>();
            CreateMap<InvoiceDto, Invoice>();
            CreateMap<InvoicePutDto, Invoice>();
            CreateMap<InvoicePostDto, Invoice>();
            CreateMap<InvoiceWithoutWorkDto, Invoice>();
        }
    }
}
