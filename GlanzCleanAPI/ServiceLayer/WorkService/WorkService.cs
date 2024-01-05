using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs;
using GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using GlanzCleanAPI.Utilities.Logging;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace GlanzCleanAPI.ServiceLayer.WorkService
{
    public class WorkService : IWorkService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public WorkService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<T> work, MetaData metaData)> GetWorkAsync<T>(WorkParameters workParameters, bool trackChanges) where T : IWorkDto
        {
            // Parameters validation
            if (!workParameters.ValidYear) throw new InvalidYearBadRequestException();
            if (workParameters.Month != null && !workParameters.ValidMonth) throw new InvalidMonthBadRequestException();
            if (workParameters.Day != null && !workParameters.ValidDay) throw new InvalidDayBadRequestException();

            // Retrieve data
            var workWithMetaData = await _repository.Work.GetWorkAsync(workParameters, trackChanges);

            // Map from entity to DTO
            var workDtos = _mapper.Map<IEnumerable<T>>(workWithMetaData);

            return (work: workDtos, metaData: workWithMetaData.MetaData);
        }

 

        public async Task<T> GetWorkByIdAsync<T>(Guid id, bool trackChanges) where T : IWorkDto
        {
            // Additional business logic
            var work = await _repository.Work.GetWorkByIdAsync(id, trackChanges) ?? throw new WorkNotFoundException(id);

            var workDto = _mapper.Map<T>(work);

            return workDto;
        }


        public async Task<WorkDto> CreateWorkAsync(WorkPostDto work)
        {
            // Check if employees exist
            foreach (Guid employeeId in work.EmployeeIDs)
            {
                var employee = await _repository.Employees.GetEmployeeByIdAsync(employeeId, false);
                if (employee is null) throw new EmployeeNotFoundException(employeeId);
            }

            // Create work
            var workEntity = _mapper.Map<Work>(work);
            _repository.Work.CreateWork(workEntity);

            // Create employee work that links employees to the newly created work
            foreach (Guid employeeId in work.EmployeeIDs)
            {
                var employeeWork = new EmployeeWorkPostDto(employeeId, workEntity.Id);
                var employeeWorkEntity = _mapper.Map<EmployeeWork>(employeeWork);
                _repository.EmployeeWork.CreateEmployeeWork(employeeWorkEntity);
            }

            await _repository.SaveAsync();

            var workToReturn = _mapper.Map<WorkDto>(workEntity);
            return workToReturn;
        }

        public async Task DeleteWorkAsync(Guid id)
        {
            // Check if the work with the given id exists
            var work = await _repository.Work.GetWorkByIdAsync(id, false);
            if (work is null) throw new WorkNotFoundException(id);

            // Check if there is at least one invoice associated with the work
            var cantDelete = await _repository.Invoices.InvoiceExistsAsync(invoice => invoice.WorkId == id);
            if (cantDelete) throw new CantDeleteWorkBadRequestException("Cannot delete work. There exists at least one invoice associated with this work!");

            // Delete all EmployeeWork associated with the current work
            var employeeWork = await _repository.EmployeeWork.GetAllFilteredAsync(w => w.WorkId == id);
            foreach( var employeeWorkEntity in employeeWork)
            {
                _repository.EmployeeWork.DeleteEmployeeWork(employeeWorkEntity);
            }

            // Delete work and save
            _repository.Work.DeleteWork(work);
            await _repository.SaveAsync();
        }

        public async Task UpdateWorkAsync(Guid id, WorkPutDto work, bool trackChanges)
        {
            var workEntity = await _repository.Work.GetWorkByIdAsync(id, trackChanges);
            if (workEntity is null) throw new WorkNotFoundException(id);

            _mapper.Map(work, workEntity);
            await _repository.SaveAsync();

        }
    }
}