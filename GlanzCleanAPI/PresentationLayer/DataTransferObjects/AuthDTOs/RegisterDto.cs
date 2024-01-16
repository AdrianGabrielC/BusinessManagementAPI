using System.ComponentModel.DataAnnotations;

namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs
{
    public class RegisterDto:IAuthDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Address { get; init; }
        public bool? IsActive { get; set; }
        public ICollection<string>? Roles { get; init; }
    }
}
