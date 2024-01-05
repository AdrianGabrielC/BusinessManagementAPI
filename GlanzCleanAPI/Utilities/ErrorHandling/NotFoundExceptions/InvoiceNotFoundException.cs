namespace GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions
{
    public class InvoiceNotFoundException : NotFoundException
    {
        public InvoiceNotFoundException(Guid invoiceId) : base($"The invoice with id {invoiceId} doesn't exist in the database.") { }

    }
}
