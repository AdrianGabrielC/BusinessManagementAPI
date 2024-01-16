using System.ComponentModel.DataAnnotations;

namespace BusinessManagementAPI.PresentationLayer.DataTransferObjects.AuthDTOs
{
    public record ChangePassDto(string UserName, string NewPassword) : IAuthDto;

}
