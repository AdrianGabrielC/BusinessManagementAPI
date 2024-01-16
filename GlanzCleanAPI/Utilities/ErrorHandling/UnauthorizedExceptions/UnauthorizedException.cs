namespace BusinessManagementAPI.Utilities.ErrorHandling.UnauthorizedExceptions
{
    public class UnauthorizedException:Exception
    {
        public UnauthorizedException(string message):base(message) { }
    }
}
