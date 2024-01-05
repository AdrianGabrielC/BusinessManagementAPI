namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.WorkBadRequestExceptions
{
    public class CantDeleteWorkBadRequestException: BadRequestException
    {
        public CantDeleteWorkBadRequestException(string message) : base(message) { }
    }
}
