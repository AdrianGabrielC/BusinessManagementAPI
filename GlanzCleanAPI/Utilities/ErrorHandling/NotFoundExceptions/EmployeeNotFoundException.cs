namespace GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(Guid employeeId) : base($"The employee with id {employeeId} doesn't exist in the database.") { }
    }
}
