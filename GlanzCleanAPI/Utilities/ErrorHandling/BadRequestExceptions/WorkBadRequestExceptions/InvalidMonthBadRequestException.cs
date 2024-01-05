namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions
{
    public class InvalidMonthBadRequestException : BadRequestException
    {
        public InvalidMonthBadRequestException() : base("Invalid month.") { }

    }
}
