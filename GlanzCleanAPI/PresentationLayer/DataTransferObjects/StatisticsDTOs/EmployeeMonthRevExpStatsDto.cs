namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class EmployeeMonthRevExpStatsDto
    {
        public int Year {  get; set; }
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public decimal Payment { get; set; }
        public decimal Profit { get; set; }

    }
}
