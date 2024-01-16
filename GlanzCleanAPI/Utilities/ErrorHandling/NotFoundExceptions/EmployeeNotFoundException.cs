namespace GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions
{
    public class EmployeeNotFoundException : NotFoundException
    {
        public EmployeeNotFoundException(string employeeId) : base($"The employee with id {employeeId} doesn't exist in the database.") { }
    }
}
