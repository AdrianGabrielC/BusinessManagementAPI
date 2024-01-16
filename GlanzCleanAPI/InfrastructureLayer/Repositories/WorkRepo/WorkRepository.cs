using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using GlanzCleanAPI.InfrastructureLayer.Repositories.BaseRepo;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.WorkRepo
{
    public class WorkRepository : RepositoryBase<Work>, IWorkRepository
    {
        public WorkRepository(GlanzCleanDbContext context) : base(context) { }

        public void CreateWork(Work work) => Create(work);

        public void DeleteWork(Work work) => Delete(work);

        public async Task<IEnumerable<Work>> GetAllWorkAsync() => await FindAll(false).ToListAsync();
        public async Task<IEnumerable<Work>> GetAllWorkAsyncFiltered(WorkParameters workParameters) => 
            await FindByCondition(w => (workParameters.Year == w.DateStartTime.Year) &&
                                                    (workParameters.Month != null ? w.DateStartTime.Month == workParameters.Month : true) &&
                                                    (workParameters.Day != null && workParameters.Month != null ? w.DateStartTime.Day == workParameters.Day : true), false)
                        .ToListAsync();

        public async Task<PagedList<Work>> GetWorkAsync(WorkParameters workParameters, bool trackChanges)
        {
            var work = await FindByCondition(w => (workParameters.Year == w.DateStartTime.Year) &&
                                                    (workParameters.Month != null ? w.DateStartTime.Month == workParameters.Month : true) &&
                                                    (workParameters.Day != null && workParameters.Month != null ? w.DateStartTime.Day == workParameters.Day : true), trackChanges)
                        .OrderBy(c => c.Id)
                        .ToListAsync();

            return PagedList<Work>.ToPagedList(work, workParameters.PageNumber, workParameters.PageSize);

        }

        public async Task<Work> GetWorkByIdAsync(Guid id, bool trackChanges) => await FindByCondition(work => work.Id.Equals(id), trackChanges)
            .FirstOrDefaultAsync();

        public void UpdateWork(Work work) => Update(work);
    }
}
