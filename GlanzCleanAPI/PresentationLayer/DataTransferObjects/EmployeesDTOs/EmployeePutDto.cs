namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.EmployeesDTOs
{
    public record EmployeePutDto(string FirstName, string LastName, bool IsActive, string Email, string Phone, string Address) : IEmployeeDto;

}
