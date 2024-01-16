using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.Utilities.RequestFeatures;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.WorkRepo
{
    public interface IWorkRepository
    {
        Task<IEnumerable<Work>> GetAllWorkAsync();
        Task<IEnumerable<Work>> GetAllWorkAsyncFiltered(WorkParameters workParameters);
        Task<PagedList<Work>> GetWorkAsync(WorkParameters workParameters, bool trackChanges);
        Task<Work> GetWorkByIdAsync(Guid workId, bool trackChanges);
        void CreateWork(Work work);
        void UpdateWork(Work work);
        void DeleteWork(Work work);
    }
}
