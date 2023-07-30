using Microsoft.EntityFrameworkCore;

namespace FlightBookingBlazorThesis.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Economy",
                    Url = "economy"
                },
                new Category 
                {
                    Id = 2,
                    Name = "Business",
                    Url = "Business"
                },
                new Category
                {
                    Id = 3,
                    Name = "First Class",
                    Url = "first-class"
                }
                );
            // Specify the SQL Server column type for the Price property
            modelBuilder.Entity<Flight>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18, 2)"); // Example: Precision of 18 and Scale of 2

            // Seed data for the Flights table
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Number = 1,
                    Destination = "Dubai",
                    Details = "Departuring from Budapest at 7:55 Arriving to Dubai at 13:30 Airlines: Fly emirates Duration: 5 hours and 35 mins",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png",
                    Price = 450,
                    CategoryId = 3
                },
                new Flight
                {
                    Number = 2,
                    Destination = "Vienna",
                    Details = "Departuring from Budapest at 8:20 Arriving to Dubai at 10:00 Airlines: Wizz Air Duration: 1 hour and 40 mins",
                    ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                    Price = 34,
                    CategoryId = 1
                },
                new Flight
                {
                    Number = 3,
                    Destination = "Copenhagen",
                    Details = "Departuring from Budapest at 13:40 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 2 hours and 10 mins",
                    ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                    Price = 80,
                    CategoryId = 1
                },
                new Flight
                {
                     Number = 4,
                     Destination = "Amsterdam",
                     Details = "Departuring from Budapest at 21:10 Arriving to Amsterdam at :0 Arlines: Wizz Air Duration: 2 hours and 10 mins",
                     ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                     Price = 69,
                     CategoryId = 1
                 },
                   new Flight
                   {
                   Number = 5,
                   Destination = "Malmo",
                   Details = "Departuring from Budapest at 3:45 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 1 hours and 50 mins",
                   ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                   Price = 39,
                   CategoryId = 1
                    },
                    new Flight
                    {
                        Number = 6,
                        Destination = "Amman",
                        Details = "Departuring from Budapest at 3:45 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 1 hours and 50 mins",
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png",
                        Price = 129,
                        CategoryId = 2
                    }
            );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}