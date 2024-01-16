namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class EmployeeStatsYearRevenueDto
    {
        public int Year { get; set; }
        public List<EmployeeMonthRevExpStatsDto> MonthsStats { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalPayment { get; set; }
    }
}
