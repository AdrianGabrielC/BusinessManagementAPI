using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using GlanzCleanAPI.InfrastructureLayer.Repositories.BaseRepo;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.EmployeeWorkRepo
{
    public class EmployeeWorkRepository : RepositoryBase<EmployeeWork>, IEmployeeWorkRepository
    {
        public EmployeeWorkRepository(GlanzCleanDbContext context) : base(context) { }

        public async Task<IEnumerable<EmployeeWork>> GetAllFilteredAsync(Expression<Func<EmployeeWork, bool>> predicate) => await FindByCondition(predicate, false)
            .OrderBy(e => e.EmployeeId)
            .ToListAsync();

        public void CreateEmployeeWork(EmployeeWork employeeWork) => Create(employeeWork);

        public void DeleteEmployeeWork(EmployeeWork employeeWork) => Delete(employeeWork);

        public async Task<PagedList<EmployeeWork>> GetEmployeeWorkAsync(EmployeeWorkParameters employeeWorkParameters, bool trackChanges)

        {
            var employeeWork = await FindAll(trackChanges)
                .OrderBy(c => c.WorkId)
                .ToListAsync();
            return PagedList<EmployeeWork>.ToPagedList(employeeWork, employeeWorkParameters.PageNumber, employeeWorkParameters.PageSize);
        }

        public async Task<EmployeeWork> GetEmployeeWorkByIdAsync(Guid id, bool trackChanges) => 
            await FindByCondition(employeeWork => employeeWork.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();

        public void UpdateEmployeeWork(EmployeeWork employeeWork, bool trackChanges) => Update(employeeWork);
    }
}
