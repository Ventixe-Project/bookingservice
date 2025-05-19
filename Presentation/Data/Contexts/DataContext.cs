using Microsoft.EntityFrameworkCore;
using Presentation.Data.Entities;

namespace Presentation.Data.Contexts
{
    public class DataContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<BookingOwnerEntity> BookingOwners { get; set; }
        public DbSet<BookingAddressEntity> BookingAddresses { get; set; }
    }
}
