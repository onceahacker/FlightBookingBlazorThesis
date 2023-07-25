﻿// <auto-generated />
using FlightBookingBlazorThesis.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlightBookingBlazorThesis.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230725222249_FlightSeeding")]
    partial class FlightSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlightBookingBlazorThesis.Shared.Flight", b =>
                {
                    b.Property<int>("Number")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Number"));

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Number");

                    b.ToTable("Flights");

                    b.HasData(
                        new
                        {
                            Number = 1,
                            Destination = "Dubai",
                            Details = "Departuring from Budapest at 7:55 Arriving to Dubai at 13:30 Airlines: Fly emirates Duration: 5 hours and 35 mins",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png",
                            Price = 64999m
                        },
                        new
                        {
                            Number = 2,
                            Destination = "Vienna",
                            Details = "Departuring from Budapest at 8:20 Arriving to Dubai at 10:00 Airlines: Wizz Air Duration: 1 hour and 40 mins",
                            ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                            Price = 8599m
                        },
                        new
                        {
                            Number = 3,
                            Destination = "Copenhagen",
                            Details = "Departuring from Budapest at 13:40 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 2 hours and 10 mins",
                            ImageUrl = "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
                            Price = 14999m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
