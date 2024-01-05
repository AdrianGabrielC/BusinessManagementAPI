namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs
{
    public record EmployeePostDto(Guid Id, string FirstName, string LastName, bool IsActive, string Email, string Phone, string Address) : IEmployeeDto;

}
