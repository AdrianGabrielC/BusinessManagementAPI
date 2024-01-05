using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.InvoiceDTOs;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using GlanzCleanAPI.Utilities.Logging;
using GlanzCleanAPI.Utilities.RequestFeatures;
using System.Linq.Expressions;

namespace GlanzCleanAPI.ServiceLayer.InvoiceService
{
    public class InvoicesService: IInvoiceService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;


        public InvoicesService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<T> invoices, MetaData metaData)> GetInvoicesAsync<T>(InvoiceParameters invoiceParameters, bool trackChanges) where T : IInvoiceDto
        {
            // Additional business logic
            var invoicesWithMetaData = await _repository.Invoices.GetAllInvoicesAsync(invoiceParameters, trackChanges);

            var invoicesDto = _mapper.Map<IEnumerable<T>>(invoicesWithMetaData);

            return (invoices: invoicesDto, metaData: invoicesWithMetaData.MetaData);
        }

        public async Task<T> GetInvoiceByIdAsync<T>(Guid id, bool trackChanges) where T : IInvoiceDto
        {
            // Additional business logic
            var invoice = await _repository.Invoices.GetInvoiceByIdAsync(id, trackChanges) ?? throw new InvoiceNotFoundException(id);

            var invoiceDto = _mapper.Map<T>(invoice);

            return invoiceDto;
        }

        public async Task<InvoiceDto> CreateInvoiceAsync(InvoicePostDto invoice)
        {
            var work = await _repository.Work.GetWorkByIdAsync(invoice.WorkId, false);
            if (work is null) throw new WorkNotFoundException(invoice.WorkId);

            var invoiceEntity = _mapper.Map<Invoice>(invoice);
            _repository.Invoices.CreateInvoice(invoiceEntity);
            await _repository.SaveAsync();

            var invoiceToReturn = _mapper.Map<InvoiceDto>(invoiceEntity);
            return invoiceToReturn;
        }

        public async Task DeleteInvoiceAsync(Guid id)
        {
            // Additional business logic
            var invoice = await _repository.Invoices.GetInvoiceByIdAsync(id, false);
            if (invoice is null) throw new InvoiceNotFoundException(id);

            _repository.Invoices.DeleteInvoice(invoice);
            await _repository.SaveAsync();

        }

        public async Task UpdateInvoiceAsync(Guid id, InvoicePutDto invoice, bool trackChanges)
        {
            // Additional business logic  
            var invoiceEntity = await _repository.Invoices.GetInvoiceByIdAsync(id, trackChanges);
            if (invoiceEntity is null) throw new InvoiceNotFoundException(id);

            _mapper.Map(invoice, invoiceEntity);
            await _repository.SaveAsync();

        }

        public async Task<bool> InvoiceExistsAsync(Expression<Func<Invoice, bool>> predicate) => await _repository.Invoices.InvoiceExistsAsync(predicate);
    }
}
