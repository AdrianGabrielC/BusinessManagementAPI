namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs
{
    public record WorkPostDto(
           DateTime DateStartTime,
           string Location,
           string Customer,
           decimal HoursWorked,
           decimal WorkBreak,
           decimal PricePerHour,
           string Status,
           List<WorkEmployeeInfo> EmployeesInfo,
           string Service) : IWorkDto;

    public record WorkEmployeeInfo(Guid EmployeeId, decimal PricePerHour);
}
