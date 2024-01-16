using AutoMapper;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using GlanzCleanAPI.Utilities.Logging;
using GlanzCleanAPI.Utilities.RequestFeatures;

namespace GlanzCleanAPI.ServiceLayer.EmployeeService
{
    public class EmployeesService: IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;

        public EmployeesService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<T> employees, MetaData metaData)> GetEmployeesAsync<T>(EmployeeParameters employeeParameters, bool trackChanges) where T : IEmployeeDto
        {
            // Additional business logic
            var employeesWithMetaData = await _repository.Employees.GetEmployeesAsync(employeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<T>>(employeesWithMetaData);

            return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);
        }

        public async Task<T> GetEmployeeByIdAsync<T>(Guid id, bool trackChanges) where T : IEmployeeDto
        {
            // Additional business logic
            var employee = await _repository.Employees.GetEmployeeByIdAsync(id, trackChanges) ?? throw new EmployeeNotFoundException(id.ToString());

            var employeeDto = _mapper.Map<T>(employee);

            return employeeDto;
        }

        public async Task<T> GetEmployeeByEmailAsync<T>(string email, bool trackChanges) where T : IEmployeeDto
        {
            var employee = await _repository.Employees.GetEmployeeByEmailAsync(email, trackChanges) ?? throw new EmployeeNotFoundException(email);

            var employeeDto = _mapper.Map<T>(employee);

            return employeeDto;
        }


        public async Task<EmployeeDto> CreateEmployeeAsync<T>(T employee) where T : IEmployeeDto
        {
            var employeeEntity = _mapper.Map<Employee>(employee);

            _repository.Employees.CreateEmployee(employeeEntity);

            await _repository.SaveAsync();

            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);

            return employeeToReturn;
        }

        public async Task DeleteEmployeeAsync(Guid id)
        {
            var employee = await _repository.Employees.GetEmployeeByIdAsync(id, false);
            if (employee is null) throw new EmployeeNotFoundException(id.ToString());

            // Check if employee is assigned to any work (cannot delete employees that are assigned to at least one work

            _repository.Employees.DeleteEmployee(employee);
            await _repository.SaveAsync();

        }

        public async Task UpdateEmployeeAsync(Guid id, EmployeePutDto employee, bool trackChanges)
        {
            var employeeEntity = await _repository.Employees.GetEmployeeByIdAsync(id, trackChanges);
            if (employeeEntity is null) throw new EmployeeNotFoundException(id.ToString());

            _mapper.Map(employee, employeeEntity);
            await _repository.SaveAsync();

        }
    }
}
