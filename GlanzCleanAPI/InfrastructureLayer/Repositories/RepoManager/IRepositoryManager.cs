using GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeRepo;
using GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeWorkRepo;
using GlanzCleanAPI.InfrastructureLayer.Repositories.InvoiceRepo;
using GlanzCleanAPI.InfrastructureLayer.Repositories.WorkRepo;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager
{
    public interface IRepositoryManager
    {
        IEmployeeRepository Employees { get; }
        IEmployeeWorkRepository EmployeeWork { get; }
        IWorkRepository Work { get; }
        IInvoiceRepository Invoices { get; }
        Task SaveAsync();
    }
}
