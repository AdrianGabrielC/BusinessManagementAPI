using BusinessManagementAPI.ServiceLayer.AuthService;
using BusinessManagementAPI.ServiceLayer.StatisticsService;
using GlanzCleanAPI.ServiceLayer.EmployeeService;
using GlanzCleanAPI.ServiceLayer.EmployeeWorkService;
using GlanzCleanAPI.ServiceLayer.InvoiceService;
using GlanzCleanAPI.ServiceLayer.WorkService;

namespace GlanzCleanAPI.ServiceLayer.ServiceManager
{
    public interface IServiceManager
    {
        IEmployeeService EmployeeService { get; }
        IEmployeeWorkService EmployeeWorkService { get; }
        IWorkService WorkService { get; }
        IInvoiceService InvoiceService { get; }
        IAuthService AuthService { get; }
        IStatisticsService StatisticsService { get; }
    }
}
