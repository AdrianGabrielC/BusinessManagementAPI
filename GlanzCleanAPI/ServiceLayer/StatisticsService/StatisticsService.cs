using BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs;
using GlanzCleanAPI.CoreLayer.Entities;
using GlanzCleanAPI.InfrastructureLayer.Repositories.RepoManager;
using GlanzCleanAPI.ServiceLayer.ServiceManager;
using GlanzCleanAPI.Utilities.RequestFeatures;
using Microsoft.IdentityModel.Tokens;

namespace BusinessManagementAPI.ServiceLayer.StatisticsService
{
    public class StatisticsService: IStatisticsService
    {
        private readonly IRepositoryManager _repository;
        public StatisticsService(IRepositoryManager repositoryManager)
        {
            _repository = repositoryManager;
        }


        public async Task<EmployeeStatsYearRevenueDto?> GetEmployeeStatsYearRevenueAsync(int year, Guid employeeId)
        {
            var employee = await _repository.Employees.GetEmployeeByIdAsync(employeeId, false);
            if (employee is null) return null;
            var employeeWork = await _repository.EmployeeWork.GetAllFilteredAsync(ew => ew.EmployeeId == employee.Id);
            if (employeeWork is null) return null;

            var employeeStats = new EmployeeStatsYearRevenueDto();
            var monthsRevExpDict = new Dictionary<int,EmployeeMonthRevExpStatsDto>();
            employeeStats.Year = year;
            foreach (var  empWork in employeeWork)
            {
                var work = await _repository.Work.GetWorkByIdAsync(empWork.WorkId, false);
                if (work.DateStartTime.Year == year)
                {
                    if (!monthsRevExpDict.ContainsKey(work.DateStartTime.Month))
                    {
                        monthsRevExpDict[work.DateStartTime.Month] = new EmployeeMonthRevExpStatsDto
                        {
                            Year = work.DateStartTime.Year,
                            Month = work.DateStartTime.Month,
                            Revenue = 0,
                            Payment = 0,
                            Profit = 0
                        };
                    }
                    // Curent values
                    var currentRevenue = work.PricePerHour * work.HoursWorked;
                    var currentPayment = empWork.PricePerHour * work.HoursWorked;
                    var currentProfit = currentRevenue - currentPayment;
                    // Current month:
                    monthsRevExpDict[work.DateStartTime.Month].Revenue += currentRevenue;
                    monthsRevExpDict[work.DateStartTime.Month].Payment += currentPayment;
                    monthsRevExpDict[work.DateStartTime.Month].Profit += currentProfit;
                    // Total:
                    employeeStats.TotalRevenue += currentRevenue;
                    employeeStats.TotalPayment += currentPayment;
                    employeeStats.TotalProfit += currentProfit;
                }
            }
            employeeStats.MonthsStats = monthsRevExpDict.Values.ToList();
            return employeeStats;
        }
        public async Task<EmployeeStatsYearHoursWorkedDto?> GetEmployeeStatsYearHoursWorkedAsync(int year, Guid employeeId)
        {
            var employee = await _repository.Employees.GetEmployeeByIdAsync(employeeId, false);
            if (employee == null) return null;
            var employeeWork = await _repository.EmployeeWork.GetAllFilteredAsync(ew => ew.EmployeeId == employee.Id);
            if (employeeWork is null) return null;

            var employeeStats = new EmployeeStatsYearHoursWorkedDto();
            var monthsHourDict = new Dictionary<int, EmployeeMonthHoursStatsDto>();

            employeeStats.Year = year;
            foreach(var empWork in employeeWork)
            {
                var work = await _repository.Work.GetWorkByIdAsync(empWork.WorkId, false);
                if (work.DateStartTime.Year == year)
                {
                    if (!monthsHourDict.ContainsKey(work.DateStartTime.Month))
                    {
                        monthsHourDict[work.DateStartTime.Month] = new EmployeeMonthHoursStatsDto
                        {
                            Year = work.DateStartTime.Year,
                            Month = work.DateStartTime.Month,
                            HoursWorked = 0
                        };
                    }
                    monthsHourDict[work.DateStartTime.Month].HoursWorked += work.HoursWorked;
                    employeeStats.TotalHoursWorked += work.HoursWorked;
                }
            }
            employeeStats.MonthsStats = monthsHourDict.Values.ToList();
            return employeeStats;
        }



        public async Task<AllYearsStatsDto?> GetAllYearsStatsAsync()
        {
            var work = await _repository.Work.GetAllWorkAsync();
            var employeeWork = await _repository.EmployeeWork.GetAllAsync();
            if (work.IsNullOrEmpty() || employeeWork.IsNullOrEmpty()) return null;

            var allYearsRevAndExpStats = work
                .GroupBy(w => w.DateStartTime.Year)
                .Select(group => new YearRevAndExpDto
                {
                    Year = group.Key,
                    Revenue = group.Sum(w => w.HoursWorked * w.PricePerHour),
                    Expenses = group
                    .Join(employeeWork, w => w.Id, ew => ew.WorkId, (w,ew) => w.HoursWorked * ew.PricePerHour)
                    .Sum()
                })
                .OrderBy(w => w.Year)
                .ToList();

            var allYearsStats = new AllYearsStatsDto()
            {
                YearsRevAndExp = allYearsRevAndExpStats,
                Peak = allYearsRevAndExpStats.Max(stats => stats.Revenue),
                Lowest = allYearsRevAndExpStats.Min(stats => stats.Revenue),
                Median = allYearsRevAndExpStats.OrderBy(stats => stats.Revenue).Skip(allYearsRevAndExpStats.Count / 2).FirstOrDefault()?.Revenue ?? 0,
                Average = allYearsRevAndExpStats.Average(stats => stats.Revenue),
                Total = allYearsRevAndExpStats.Sum(stats => stats.Revenue),
                AGR = ((allYearsRevAndExpStats.Last().Revenue - allYearsRevAndExpStats.First().Revenue) / allYearsRevAndExpStats.First().Revenue) * 100
            };
            return allYearsStats;
        }

        public async Task<List<YearStatsDto>?> GetYearStatsAsync(int? year)
        {

            // The keys of this dict represent months and the values are YearStatsDto objects. 
            // Each YearStatsDto object is associated with a month and has Revenue and Expenses as properties for that month.
            Dictionary<int, YearStatsDto> yearStatsDtoDict = new Dictionary<int, YearStatsDto>();

            // Get all work
            var workParameters = new WorkParameters();
            if (year is not null) workParameters.Year = year ?? DateTime.Now.Year;

            
            var work = await _repository.Work.GetAllWorkAsyncFiltered(workParameters);
            if (work.IsNullOrEmpty()) return null;
            
            
            foreach (var workItem in work)
            {
                // Get the month of the current work 
                var workMonth = workItem.DateStartTime.Month;

                // Check if a YearStatsDto exists in the dict with the key == month of current work
                if (!yearStatsDtoDict.ContainsKey(workMonth))
                {
                    yearStatsDtoDict[workMonth] = new YearStatsDto
                    {
                        Year = workItem.DateStartTime.Year,
                        Month = workMonth,
                        Revenue = 0,
                        Expenses = 0
                    };
                }

                // Add to the revenue of the workMonth the revenue generated by the current work
                yearStatsDtoDict[workMonth].Revenue += workItem.HoursWorked * workItem.PricePerHour;

                // Compute the expenses for the current work by retrieving the EmployeeWork done by each employee the current work and adding the amount payed for each employee
                var employeeWork = await _repository.EmployeeWork.GetAllFilteredAsync(e => e.WorkId == workItem.Id);
                foreach (var employeeWorkItem in employeeWork)
                {
                    yearStatsDtoDict[workMonth].Expenses += employeeWorkItem.PricePerHour * workItem.HoursWorked;
                }

            }

            // Return a list with YearStatsDto 
            return new List<YearStatsDto>(yearStatsDtoDict.Values);
        }

    }
}
