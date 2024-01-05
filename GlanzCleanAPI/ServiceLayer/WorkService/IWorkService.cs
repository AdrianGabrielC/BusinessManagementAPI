using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs;
using GlanzCleanAPI.Utilities.RequestFeatures;

namespace GlanzCleanAPI.ServiceLayer.WorkService
{
    public interface IWorkService
    {
        Task<(IEnumerable<T> work, MetaData metaData)> GetWorkAsync<T>(WorkParameters workParameters, bool trackChanges) where T : IWorkDto;
        Task<T> GetWorkByIdAsync<T>(Guid id, bool trackChanges) where T : IWorkDto;
        Task<WorkDto> CreateWorkAsync(WorkPostDto work);
        Task UpdateWorkAsync(Guid id, WorkPutDto work, bool trackChanges);
        Task DeleteWorkAsync(Guid id);

    }
}
