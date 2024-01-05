namespace GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions
{
    public abstract class BadRequestException : Exception
    {
        protected BadRequestException(string message) : base(message) { }
    }
}
