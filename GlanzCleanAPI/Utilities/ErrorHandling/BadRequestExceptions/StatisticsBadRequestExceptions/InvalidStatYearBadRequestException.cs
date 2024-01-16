namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions
{
    public class InvalidStatYearBadRequestException : BadRequestException
    {
        public InvalidStatYearBadRequestException(): base("Invalid year") { }
    }
}
