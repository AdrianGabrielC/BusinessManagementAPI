namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs
{
    public record WorkPutDto(
           DateTime DateStartTime,
           string Location,
           string Customer,
           decimal HoursWorked,
           decimal WorkBreak,
           decimal PricePerHour,
           string Status,
           List<Guid> EmployeeIDs,
           string Service) : IWorkDto;
}
