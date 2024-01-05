using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.InvoiceRepo
{
    public interface IInvoiceRepository
    {
        Task<PagedList<Invoice>> GetAllInvoicesAsync(InvoiceParameters invoiceParameters, bool trackChanges);
        Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId, bool trackChanges);
        void CreateInvoice(Invoice invoice);
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(Invoice invoice);
        Task<bool> InvoiceExistsAsync(Expression<Func<Invoice, bool>> predicate);

    }
}
