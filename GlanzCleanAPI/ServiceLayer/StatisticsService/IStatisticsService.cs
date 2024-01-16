using BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs;

namespace BusinessManagementAPI.ServiceLayer.StatisticsService
{
    public interface IStatisticsService
    {
        Task<AllYearsStatsDto?> GetAllYearsStatsAsync();
        Task<List<YearStatsDto>?> GetYearStatsAsync(int? year);
        Task<EmployeeStatsYearRevenueDto?> GetEmployeeStatsYearRevenueAsync(int year, Guid employeeId);
        Task<EmployeeStatsYearHoursWorkedDto?> GetEmployeeStatsYearHoursWorkedAsync(int year, Guid employeeId);
    }
}
