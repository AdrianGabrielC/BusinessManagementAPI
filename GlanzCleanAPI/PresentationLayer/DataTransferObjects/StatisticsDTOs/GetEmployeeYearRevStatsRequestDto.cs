namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.StatisticsDTOs
{
    public class GetEmployeeYearRevStatsRequestDto
    {
        public int Year { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
