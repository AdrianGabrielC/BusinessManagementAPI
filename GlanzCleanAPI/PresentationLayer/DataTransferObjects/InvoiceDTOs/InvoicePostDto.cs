
namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs
{
    public record InvoicePostDto(Guid Id, Guid WorkId) : IInvoiceDto;

}
