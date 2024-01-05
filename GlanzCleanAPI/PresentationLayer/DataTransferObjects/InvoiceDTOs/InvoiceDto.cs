using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs;

namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs
{
    public record InvoiceDto(Guid Id, Guid WorkId, WorkDto Work) : IInvoiceDto;

}
