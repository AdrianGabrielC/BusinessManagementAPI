using GlanzCleanAPI.Utilities.ErrorHandling.BadRequestExceptions;

namespace BusinessManagementAPI.Utilities.ErrorHandling.BadRequestExceptions.AuthBadRequestExceptions
{
    public class AuthInvalidDataBadRequest:BadRequestException
    {
        public AuthInvalidDataBadRequest(string message) : base(message) { }
    }
}
