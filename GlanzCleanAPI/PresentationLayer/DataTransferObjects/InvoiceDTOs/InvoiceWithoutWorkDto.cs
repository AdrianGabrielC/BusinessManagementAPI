namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs
{
    public record InvoiceWithoutWorkDto(Guid Id, Guid WorkId) : IInvoiceDto;

}
