using Microsoft.EntityFrameworkCore;

namespace FlightBookingBlazorThesis.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Flight> Flights { get; set; }
    }
}
