using System.ComponentModel.DataAnnotations;

namespace GlanzCleanAPI.CoreLayer.Entities
{
    public class Work
    {
        public Guid Id { get; set; }
        public DateTime DateStartTime { get; set; }
        [Required(ErrorMessage = "Location is a required field.")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Customer is a required field.")]
        public string Customer { get; set; }
        [Required(ErrorMessage = "Hours worked is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Hours worked cannot be less than 0 minutes.")]
        public int HoursWorked { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Work break cannot be less than 0 minutes.")]
        public int WorkBreak { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Price per hour cannot be less than 0.")]
        public decimal PricePerHour { get; set; }
        public string Service { get; set; }
        public string Status {  get; set; }
        public virtual List<Invoice>? Invoices { get; set; } // Nav property to get all invoices of a work
        public virtual List<EmployeeWork> EmployeeWork { get; set; }
    }
}
