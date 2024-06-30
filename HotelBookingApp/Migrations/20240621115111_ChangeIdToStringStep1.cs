using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIdToStringStep1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Δημιουργία νέας προσωρινής στήλης
            migrationBuilder.AddColumn<string>(
                name: "TempId",
                table: "AspNetUsers",
                nullable: true);

            // Αντιγραφή δεδομένων από την παλιά στήλη Id στη νέα στήλη TempId
            migrationBuilder.Sql("UPDATE AspNetUsers SET TempId = CAST(Id AS NVARCHAR(450))");

            // Δημιουργία νέας προσωρινής στήλης για Accommodations
            migrationBuilder.AddColumn<string>(
                name: "TempAccommodationId",
                table: "Accommodations",
                nullable: true);

            // Αντιγραφή δεδομένων από την παλιά στήλη AccommodationId στη νέα στήλη TempAccommodationId
            migrationBuilder.Sql("UPDATE Accommodations SET TempAccommodationId = CAST(AccommodationId AS NVARCHAR(450))");

            // Δημιουργία νέας προσωρινής στήλης για Bookings
            migrationBuilder.AddColumn<string>(
                name: "TempBookingId",
                table: "Bookings",
                nullable: true);

            // Αντιγραφή δεδομένων από την παλιά στήλη BookingId στη νέα στήλη TempBookingId
            migrationBuilder.Sql("UPDATE Bookings SET TempBookingId = CAST(BookingId AS NVARCHAR(450))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "TempId", table: "AspNetUsers");
            migrationBuilder.DropColumn(name: "TempAccommodationId", table: "Accommodations");
            migrationBuilder.DropColumn(name: "TempBookingId", table: "Bookings");
        }

    }
}
