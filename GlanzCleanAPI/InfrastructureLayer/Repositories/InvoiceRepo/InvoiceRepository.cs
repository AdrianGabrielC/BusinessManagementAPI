using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using GlanzCleanAPI.InfrastructureLayer.Repositories.BaseRepo;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.InvoiceRepo
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(GlanzCleanDbContext context) : base(context) { }

        public void CreateInvoice(Invoice invoice) => Create(invoice);

        public void DeleteInvoice(Invoice invoice) => Delete(invoice);

        public async Task<PagedList<Invoice>> GetAllInvoicesAsync(InvoiceParameters invoiceParameters, bool trackChanges)
        {
            var invoices = await FindAll(trackChanges)
                .Include(i => i.Work)
                .OrderBy(c => c.Id)
                .ToListAsync();

            return PagedList<Invoice>.ToPagedList(invoices, invoiceParameters.PageNumber, invoiceParameters.PageSize);
        }

        public async Task<Invoice> GetInvoiceByIdAsync(Guid id, bool trackChanges) => await FindByCondition(invoice => invoice.Id.Equals(id), trackChanges)
             .Include(i => i.Work)
            .FirstOrDefaultAsync();

        public void UpdateInvoice(Invoice invoice) => Update(invoice);

        public async Task<bool> InvoiceExistsAsync(Expression<Func<Invoice, bool>> predicate) => await _context.Invoices.AnyAsync(predicate);
    }
}
