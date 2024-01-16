namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs
{
    public record EmployeeWorkDto(Guid Id, Guid EmployeeId, Guid WorkId, decimal PricePerHour) : IEmployeeWorkDto;

}
