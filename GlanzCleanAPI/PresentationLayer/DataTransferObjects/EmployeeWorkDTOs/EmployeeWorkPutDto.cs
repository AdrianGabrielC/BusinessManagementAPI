namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeeWorkDTOs
{
    public record EmployeeWorkPutDto(Guid EmployeeId, Guid WorkId) : IEmployeeWorkDto;

}
