using System.ComponentModel.DataAnnotations;

namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs
{
    public class LoginDto:IAuthDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? UserName { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
    }
}
