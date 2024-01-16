using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;

namespace BusinessManagementAPI.Utilities.ErrorHandling.NotFoundExceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User not found") { }
    }
}
