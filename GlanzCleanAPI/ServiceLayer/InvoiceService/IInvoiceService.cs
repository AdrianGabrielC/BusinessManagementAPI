using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs;
using GlanzCleanAPI.Utilities.RequestFeatures;
using System.Linq.Expressions;

namespace GlanzCleanAPI.ServiceLayer.InvoiceService
{
    public interface IInvoiceService
    {
        Task<(IEnumerable<T> invoices, MetaData metaData)> GetInvoicesAsync<T>(InvoiceParameters invoiceParameters, bool trackChanges) where T : IInvoiceDto;
        Task<T> GetInvoiceByIdAsync<T>(Guid id, bool trackChanges) where T : IInvoiceDto;
        Task<InvoiceDto> CreateInvoiceAsync(InvoicePostDto invoice);
        Task UpdateInvoiceAsync(Guid id, InvoicePutDto invoice, bool trackChanges);
        Task DeleteInvoiceAsync(Guid id);
        Task<bool> InvoiceExistsAsync(Expression<Func<Invoice, bool>> predicate);
    }
}
