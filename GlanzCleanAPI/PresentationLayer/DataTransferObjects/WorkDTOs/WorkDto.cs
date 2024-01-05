namespace GlanzCleanAPI.PresentationLayer.DataTransferObjects.WorkDTOs
{
    public record WorkDto(Guid Id,
         DateTime DateStartTime,
         string Location,
         string Customer,
         decimal HoursWorked,
         decimal WorkBreak,
         decimal PricePerHour,
         string Status,
         string Service) : IWorkDto;
}
