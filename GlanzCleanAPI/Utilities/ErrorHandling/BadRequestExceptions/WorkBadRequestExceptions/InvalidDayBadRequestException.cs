namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions
{
    public class InvalidDayBadRequestException : BadRequestException
    {
        public InvalidDayBadRequestException() : base("Invalid day.") { }
    }
}
