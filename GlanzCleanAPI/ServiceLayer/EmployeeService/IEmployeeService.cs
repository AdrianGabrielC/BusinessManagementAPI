using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.Utilities.RequestFeatures;

namespace GlanzCleanAPI.ServiceLayer.EmployeeService
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<T> employees, MetaData metaData)> GetEmployeesAsync<T>(EmployeeParameters employeeParameters, bool trackChanges) where T : IEmployeeDto;
        Task<T> GetEmployeeByIdAsync<T>(Guid id, bool trackChanges) where T : IEmployeeDto;
        Task<T> GetEmployeeByEmailAsync<T>(string email, bool trackChanges)where T : IEmployeeDto;
        Task<EmployeeDto> CreateEmployeeAsync<T>(T employee) where T : IEmployeeDto;
        Task UpdateEmployeeAsync(Guid id, EmployeePutDto employee, bool trackChanges);
        Task DeleteEmployeeAsync(Guid id);
    }
}
