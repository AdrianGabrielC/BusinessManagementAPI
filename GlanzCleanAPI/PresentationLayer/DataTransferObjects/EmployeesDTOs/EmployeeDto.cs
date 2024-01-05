namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs
{
    public record EmployeeDto(Guid Id, string FirstName, string LastName, bool IsActive, string Email, string Phone, string Address) : IEmployeeDto;

}
