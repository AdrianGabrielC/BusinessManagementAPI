namespace GlanzCleanAPI.CoreLayer.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }
        public Guid WorkId { get; set; }
        public virtual Work Work { get; set; } // Nav property to get the work associatd with the invoice
    }
}
