namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions
{
    public class InvalidYearBadRequestException : BadRequestException
    {
        public InvalidYearBadRequestException() : base("Invalid year.") { }

    }
}
