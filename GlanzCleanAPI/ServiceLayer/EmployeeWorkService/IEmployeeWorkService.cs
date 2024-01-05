using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs;
using GlanzCleanAPI.Utilities.RequestFeatures;
using System.Linq.Expressions;

namespace GlanzCleanAPI.ServiceLayer.EmployeeWorkService
{
    public interface IEmployeeWorkService
    {
        Task<IEnumerable<EmployeeWork>> GetAllFilteredAsync(Expression<Func<EmployeeWork, bool>> predicate);
        Task<(IEnumerable<T> employeeWork, MetaData metaData)> GetEmployeeWorkAsync<T>(EmployeeWorkParameters employeeWorkParameters, bool trackChanges) where T : IEmployeeWorkDto;
        Task<T> GetEmployeeWorkByIdAsync<T>(Guid id, bool trackChanges) where T : IEmployeeWorkDto;
        Task<EmployeeWorkDto> CreateEmployeeWorkAsync<T>(T employeeWork) where T : IEmployeeWorkDto;
        Task UpdateEmployeeWorkAsync(Guid id, EmployeeWorkPutDto employeeWork, bool trackChanges);
        Task DeleteEmployeeWorkAsync(Guid id);
    }
}
