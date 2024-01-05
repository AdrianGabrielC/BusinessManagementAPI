using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using GlanzCleanAPI.Utilities.Logging;
using GlanzCleanAPI.Utilities.RequestFeatures;
using System.Linq.Expressions;

namespace GlanzCleanAPI.ServiceLayer.EmployeeWorkService
{
    public class EmployeeWorkService: IEmployeeWorkService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public EmployeeWorkService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeWork>> GetAllFilteredAsync(Expression<Func<EmployeeWork, bool>> predicate) => await _repository.EmployeeWork.GetAllFilteredAsync(predicate);
        public async Task<(IEnumerable<T> employeeWork, MetaData metaData)> GetEmployeeWorkAsync<T>(EmployeeWorkParameters employeeWorkParameters, bool trackChanges) where T : IEmployeeWorkDto
        {
            // Additional business logic
            var employeesWithMetaData = await _repository.EmployeeWork.GetEmployeeWorkAsync(employeeWorkParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<T>>(employeesWithMetaData);

            return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<T> GetEmployeeWorkByIdAsync<T>(Guid id, bool trackChanges) where T : IEmployeeWorkDto
        {
            // Additional business logic
            var employeeWork = await _repository.EmployeeWork.GetEmployeeWorkByIdAsync(id, trackChanges) ?? throw new EmployeeWorkNotFoundException(id);

            var employeeDto = _mapper.Map<T>(employeeWork);

            return employeeDto;
        }

        public async Task<EmployeeWorkDto> CreateEmployeeWorkAsync<T>(T employeeWork) where T : IEmployeeWorkDto
        {
            var employeeEntity = _mapper.Map<EmployeeWork>(employeeWork);

            _repository.EmployeeWork.CreateEmployeeWork(employeeEntity);

            await _repository.SaveAsync();

            var employeeWorkToReturn = _mapper.Map<EmployeeWorkDto>(employeeEntity);

            return employeeWorkToReturn;
        }

        public async Task DeleteEmployeeWorkAsync(Guid id)
        {
            var employeeWork = await _repository.EmployeeWork.GetEmployeeWorkByIdAsync(id, false);
            if (employeeWork is null) throw new EmployeeWorkNotFoundException(id);

            // Check if employee is assigned to any work (cannot delete employees that are assigned to at least one work

            _repository.EmployeeWork.DeleteEmployeeWork(employeeWork);
            await _repository.SaveAsync();

        }

        public async Task UpdateEmployeeWorkAsync(Guid id, EmployeeWorkPutDto employeeWork, bool trackChanges)
        {
            var employeeEntity = await _repository.EmployeeWork.GetEmployeeWorkByIdAsync(id, trackChanges);
            if (employeeEntity is null) throw new EmployeeWorkNotFoundException(id);
            employeeEntity.EmployeeId = employeeWork.EmployeeId;
            employeeEntity.WorkId = employeeWork.WorkId;
            await _repository.SaveAsync();

        }
    }
}
