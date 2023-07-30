using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlightBookingBlazorThesis.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 1,
                columns: new[] { "CategoryId", "Price" },
                values: new object[] { 3, 450m });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 2,
                column: "Price",
                value: 34m);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 3,
                column: "Price",
                value: 80m);

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Number", "CategoryId", "Destination", "Details", "ImageUrl", "Price" },
                values: new object[,]
                {
                    { 4, 1, "Amsterdam", "Departuring from Budapest at 21:10 Arriving to Amsterdam at :0 Arlines: Wizz Air Duration: 2 hours and 10 mins", "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg", 69m },
                    { 5, 1, "Malmo", "Departuring from Budapest at 3:45 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 1 hours and 50 mins", "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg", 39m },
                    { 6, 2, "Amman", "Departuring from Budapest at 3:45 Arriving to Dubai at 15:50 Airlines: Wizz Air Duration: 1 hours and 50 mins", "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png", 129m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 1,
                columns: new[] { "CategoryId", "Price" },
                values: new object[] { 1, 64999m });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 2,
                column: "Price",
                value: 8599m);

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Number",
                keyValue: 3,
                column: "Price",
                value: 14999m);
        }
    }
}
