namespace GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions
{
    public class EmployeeWorkNotFoundException : NotFoundException
    {
        public EmployeeWorkNotFoundException(Guid employeeWorkId) : base($"The employee work with id {employeeWorkId} doesn't exist in the database.") { }
    }
}
