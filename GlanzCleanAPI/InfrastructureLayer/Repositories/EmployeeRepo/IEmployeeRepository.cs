using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.Utilities.RequestFeatures;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeRepo
{
    public interface IEmployeeRepository
    {
        Task<PagedList<Employee>> GetEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges);
        Task<Employee> GetEmployeeByIdAsync(Guid employeeId, bool trackChanges);
        Task<Employee> GetEmployeeByEmailAsync(string employeeEmail, bool trackChanges);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
