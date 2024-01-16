using Microsoft.AspNetCore.Identity;

namespace BusinessManagementAPI.CoreLayer.Entities
{
    public class User : IdentityUser
    {
        public Guid EmployeeId { get; set; }
    }
}
