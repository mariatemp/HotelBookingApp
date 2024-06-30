using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingApp.Migrations
{
    public partial class ChangeUserIdTypeToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add a temporary column to store the new UserId
            migrationBuilder.AddColumn<string>(
                name: "TempUserId",
                table: "Users",
                nullable: true);

            // Copy values from UserId to TempUserId
            migrationBuilder.Sql("UPDATE Users SET TempUserId = CAST(UserId AS nvarchar(max))");

            // Drop the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            // Drop the old UserId column
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            // Rename TempUserId to UserId
            migrationBuilder.RenameColumn(
                name: "TempUserId",
                table: "Users",
                newName: "UserId");

            // Alter the new UserId column to be non-nullable
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            // Add the primary key constraint back to the UserId column
            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TempUserId",
                table: "Users",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");
        }
    }
}

