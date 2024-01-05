using System.ComponentModel.DataAnnotations;

namespace GlanzCleanAPI.CoreLayer.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First name is a required field.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is a required field.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public virtual List<EmployeeWork> Works { get; set; } // Nav property to get all work associated with an employee
    }
}
