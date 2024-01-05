namespace GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions
{
    public class WorkNotFoundException : NotFoundException
    {
        public WorkNotFoundException(Guid workId) : base($"The work with id {workId} doesn't exist in the database.") { }

    }
}
