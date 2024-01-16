namespace GlanzCleanAPI.CoreLayer.Entities
{
    public class EmployeeWork
    {
        public Guid Id { get; set; }
        public decimal PricePerHour { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public Guid WorkId { get; set; }
        public virtual Work Work { get; set; }
    }
}
