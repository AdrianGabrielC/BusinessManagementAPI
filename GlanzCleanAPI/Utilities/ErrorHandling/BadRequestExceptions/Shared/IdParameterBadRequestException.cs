namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions.Shared
{
    public class IdParameterBadRequestException : BadRequestException
    {
        public IdParameterBadRequestException(string message) : base(message) { }
    }
}
