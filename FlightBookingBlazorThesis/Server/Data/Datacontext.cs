﻿using Microsoft.EntityFrameworkCore;

namespace FlightBookingBlazorThesis.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CartItem>()
        .HasKey(ci => new { ci.UserId, ci.FlightId, ci.FlightTypeId });

            modelBuilder.Entity<FlightVariant>()
                .HasKey(f => new { f.FlightId, f.FlightTypeId });

            modelBuilder.Entity<OrderItem>()
               .HasKey(oi => new { oi.OrderId, oi.FlightId, oi.FlightTypeId });

            modelBuilder.Entity<FlightType>().HasData(
                new FlightType { Id = 1, Name = "One Way"},
                new FlightType { Id = 2, Name = "Round Trip" },
                new FlightType { Id = 3, Name = "Multi-City" }


                );

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


            // Seed data for the Flights table
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Number = 1,
                    Destination = "Dubai",
                    Details = "Non-stop, Wi-Fi available, In-flight meals",
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png",
                    CategoryId = 3,
                    DepartureDate = new DateTime(2023, 08, 15, 7, 55, 0),
                },
                new Flight
                {
                    Number = 2,
                    Destination = "New York",
                    Details = "Layover, In-flight entertainment",
                    ImageUrl = "https://logos-world.net/wp-content/uploads/2021/08/Delta-Logo.png",
                    CategoryId = 2,
                    DepartureDate = new DateTime(2023, 08, 20, 10, 0, 0)
                },
                new Flight
                {
                    Number = 3,
                    Destination = "London",
                    Details = "Non-stop, Extra legroom",
                    ImageUrl = "https://www.thephoenix.ie/wp-content/uploads/2021/01/Ryanair-logo.png",
                    CategoryId = 1,
                    DepartureDate = new DateTime(2023, 08, 25, 14, 30, 0),
                },
                new Flight
                {
                     Number = 4,
                     Destination = "Amsterdam",
                     Details = "Departuring from Budapest at 21:10 Arriving to Amsterdam at :0 Arlines: Wizz Air Duration: 2 hours and 10 mins",
                     ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                     CategoryId = 1,
                 },
                   new Flight
                   {
                       Number = 5,
                       Destination = "Tokyo",
                       Details = "Non-stop, Wi-Fi available",
                       ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Finnair_Logo.svg/2560px-Finnair_Logo.svg.png",
                       CategoryId = 3,
                       DepartureDate = new DateTime(2023, 09, 10, 12, 45, 0)
                   },
                    new Flight
                    {
                        Number = 6,
                        Destination = "Sydney",
                        Details = "Non-stop, In-flight meals",
                        ImageUrl = "data:image/png;base64,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",
                        CategoryId = 3,
                        DepartureDate = new DateTime(2023, 09, 15, 8, 30, 0)
                    }
            );

            modelBuilder.Entity<FlightVariant>().HasData(
                new FlightVariant
                {
                    FlightId = 1,
                    FlightTypeId = 1,
                    Price = 400,
                    OriginalPrice = 500
                },
                 new FlightVariant
                 {
                     FlightId = 1,
                     FlightTypeId = 2,
                     Price = 350,
                     OriginalPrice = 450
                 },
                  new FlightVariant
                  {
                      FlightId = 2,
                      FlightTypeId = 1,
                      Price = 550,
                      OriginalPrice = 650
                  },
                   new FlightVariant
                   {
                       FlightId = 2,
                       FlightTypeId = 2,
                       Price = 500,
                       OriginalPrice = 600
                   },
                    new FlightVariant
                    {
                        FlightId = 3,
                        FlightTypeId = 1,
                        Price = 450,
                        OriginalPrice = 550
                    },
                     new FlightVariant
                     {
                         FlightId = 3,
                         FlightTypeId = 2,
                         Price = 400,
                         OriginalPrice = 500
                     },
                      new FlightVariant
                      {
                          FlightId = 4,
                          FlightTypeId = 1,
                          Price = 600,
                          OriginalPrice = 700
                      },
                      new FlightVariant
                      {
                          FlightId = 4,
                          FlightTypeId = 2,
                          Price = 550,
                          OriginalPrice = 650
                      }
                 );

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<FlightType> FlightTypes { get; set; }
        public DbSet<FlightVariant> FlightVariants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }




    }
}