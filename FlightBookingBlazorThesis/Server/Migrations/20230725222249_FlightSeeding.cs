using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightBookingBlazorThesis.Server.Migrations
{
    /// <inheritdoc />
    public partial class FlightSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Number", "Destination", "Details", "ImageUrl", "Price" },
                values: new object[,]
                {
                    { 1, "Dubai", "Departuring from Budapest at 7:55 Arriving to Dubai at 13:30 Airlines: Fly emirates Duration: 5 hours and 35 mins", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png", 64999m },
                    { 2, "Vienna", "Departuring from Budapest at 8:20 Arriving to Dubai at 10:00 Airlines: Wizz Air Duration: 1 hour and 40 mins", "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg", 8599m },
                    { 3, "Copenhagen", "Departuring from Budapest at 13:40 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 2 hours and 10 mins", "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg", 14999m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 3);
        }
    }
}
