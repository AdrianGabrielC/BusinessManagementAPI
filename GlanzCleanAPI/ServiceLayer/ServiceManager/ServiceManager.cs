using AutoMapper;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.ServiceLayer.EmployeeService;
using GlanzCleanAPI.ServiceLayer.EmployeeWorkService;
using GlanzCleanAPI.ServiceLayer.InvoiceService;
using GlanzCleanAPI.ServiceLayer.WorkService;
using GlanzCleanAPI.Utilities.Logging;

namespace GlanzCleanAPI.ServiceLayer.ServiceManager
{
    public class ServiceManager: IServiceManager
    {
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IEmployeeWorkService> _employeeWorkService;
        private readonly Lazy<IWorkService> _workService;
        private readonly Lazy<IInvoiceService> _invoiceService;

        public IEmployeeService EmployeeService => _employeeService.Value;
        public IEmployeeWorkService EmployeeWorkService => _employeeWorkService.Value;
        public IWorkService WorkService => _workService.Value;
        public IInvoiceService InvoiceService => _invoiceService.Value; 

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeesService(repositoryManager, mapper));
            _employeeWorkService = new Lazy<IEmployeeWorkService>(() => new GlanzCleanAPI.ServiceLayer.EmployeeWorkService.EmployeeWorkService(repositoryManager, mapper));
            _workService = new Lazy<IWorkService>(() => new GlanzCleanAPI.ServiceLayer.WorkService.WorkService(repositoryManager, mapper));
            _invoiceService = new Lazy<IInvoiceService>(() => new GlanzCleanAPI.ServiceLayer.InvoiceService.InvoicesService(repositoryManager, mapper)); 
        }
    }
}
