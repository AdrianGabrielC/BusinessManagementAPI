using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.Utilities.RequestFeatures;
using System.Linq.Expressions;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeWorkRepo
{
    public interface IEmployeeWorkRepository
    {
        Task<IEnumerable<EmployeeWork>> GetAllAsync();
        Task<IEnumerable<EmployeeWork>> GetAllFilteredAsync(Expression<Func<EmployeeWork, bool>> predicate);
        Task<PagedList<EmployeeWork>> GetEmployeeWorkAsync(EmployeeWorkParameters employeeWorkParameters, bool trackChanges);
        Task<EmployeeWork> GetEmployeeWorkByIdAsync(Guid employeeWorkId, bool trackChanges);
        void CreateEmployeeWork(EmployeeWork employeeWork);
        void UpdateEmployeeWork(EmployeeWork employeeWork, bool trackChanges);
        void DeleteEmployeeWork(EmployeeWork employeeWork);
    }
}
