namespace Presentation.Models
{
    public class BookingModel
    {
        public string Id { get; set; } = null!;
        public string EventId { get; set; } = null!;
        public string EventName { get; set; } = null!;
        public string PackageId { get; set; } = null!;
        public string PackageName { get; set; } = null!;
        public int TicketQuantity { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
    }
}
