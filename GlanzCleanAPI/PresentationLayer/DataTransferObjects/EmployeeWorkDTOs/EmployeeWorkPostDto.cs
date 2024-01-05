namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs
{
    public record EmployeeWorkPostDto(Guid EmployeeId, Guid WorkId) : IEmployeeWorkDto;

}
