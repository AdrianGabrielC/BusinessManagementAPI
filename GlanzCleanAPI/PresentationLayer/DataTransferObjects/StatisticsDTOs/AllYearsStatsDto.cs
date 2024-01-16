namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class AllYearsStatsDto: IStatsDto
    {
        public List<YearRevAndExpDto> YearsRevAndExp {  get; set; }
        public decimal Average { get; set; }
        public decimal Peak { get; set; }
        public decimal Lowest { get; set; }
        public decimal Total { get; set; }
        public decimal Median { get; set; }
        public decimal AGR { get; set;}
    }
}
