using GlanzCleanAPI.InfrastructureLayer.DbContext;
using GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeRepo;
using GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeWorkRepo;
using GlanzCleanAPI.InfrastructureLayer.Repositories.InvoiceRepo;
using GlanzCleanAPI.InfrastructureLayer.Repositories.WorkRepo;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager
{
    public class RepositoryManager: IRepositoryManager
    {
        private GlanzCleanDbContext _context;
        private Lazy<IEmployeeRepository> _employeeRepository;
        private Lazy<IEmployeeWorkRepository> _employeeWorkRepository;
        private Lazy<IWorkRepository> _workRepository;
        private Lazy<IInvoiceRepository> _invoiceRepository;

        public IEmployeeRepository Employees => _employeeRepository.Value;
        public IEmployeeWorkRepository EmployeeWork => _employeeWorkRepository.Value;
        public IWorkRepository Work => _workRepository.Value;
        public IInvoiceRepository Invoices => _invoiceRepository.Value;

        public RepositoryManager(GlanzCleanDbContext context)
        {
            _context = context;
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));
            _employeeWorkRepository = new Lazy<IEmployeeWorkRepository>(() => new EmployeeWorkRepository(context));
            _workRepository = new Lazy<IWorkRepository>(() => new WorkRepository(context));
            _invoiceRepository = new Lazy<IInvoiceRepository>(() => new InvoiceRepository(context));
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();

    }
}
