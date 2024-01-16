namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class EmployeeStatsYearHoursWorkedDto
    {
        public int Year {  get; set; }
        public List<EmployeeMonthHoursStatsDto> MonthsStats { get; set; }
        public int TotalHoursWorked { get; set; }
    }
}
