using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using GlanzCleanAPI.InfrastructureLayer.Repositories.BaseRepo;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeRepo
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(GlanzCleanDbContext context) : base(context) { }

        public void CreateEmployee(Employee employee) => Create(employee);


        public void DeleteEmployee(Employee employee) => Delete(employee);

        public async Task<PagedList<Employee>> GetEmployeesAsync(EmployeeParameters employeeParameters, bool trackChanges)
        {
            var employees = await FindAll(trackChanges)
                .OrderBy(c => c.LastName)
                .ToListAsync();
            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);
        }

        public async Task<Employee> GetEmployeeByIdAsync(Guid id, bool trackChanges) =>
            await FindByCondition(employee => employee.Id.Equals(id), trackChanges)
                .FirstOrDefaultAsync();

        public async Task<Employee> GetEmployeeByEmailAsync(string employeeEmail, bool trackChanges) => 
            await FindByCondition(employee => employee.Email.Equals(employeeEmail), trackChanges)
            .FirstOrDefaultAsync();

        public void UpdateEmployee(Employee employee) => Update(employee);

    }
}
