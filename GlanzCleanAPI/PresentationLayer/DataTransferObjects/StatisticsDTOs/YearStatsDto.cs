namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class YearStatsDto() : IStatsDto
    {
        public int Year { get; set; } 
        public int Month { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expenses { get; set; }  

    }
}
