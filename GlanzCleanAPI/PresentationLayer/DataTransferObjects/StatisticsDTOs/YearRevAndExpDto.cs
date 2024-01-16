namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class YearRevAndExpDto:IStatsDto
    {
        public int Year {  get; set; }
        public decimal Revenue {  get; set; }
        public decimal Expenses { get; set; }
    }
}
