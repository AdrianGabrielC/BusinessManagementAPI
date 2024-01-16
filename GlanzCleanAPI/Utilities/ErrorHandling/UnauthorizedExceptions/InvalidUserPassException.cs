namespace BusinessManagementAPI.Utilities.ErrorHandling.UnauthorizedExceptions
{
    public class InvalidUserPassException:UnauthorizedException
    {
        public InvalidUserPassException() : base("Invalid email or password") { }
    }
}
