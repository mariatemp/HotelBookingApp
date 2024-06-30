using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelBookingApp.Migrations
{
    public partial class RemoveUserConstraintsAndIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key constraints if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Bookings_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[Bookings]'))
                BEGIN
                    ALTER TABLE [Bookings] DROP CONSTRAINT [FK_Bookings_Users_UserId];
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Accommodations_Users_UserId]') AND parent_object_id = OBJECT_ID(N'[Accommodations]'))
                BEGIN
                    ALTER TABLE [Accommodations] DROP CONSTRAINT [FK_Accommodations_Users_UserId];
                END
            ");

            // Drop indexes if they exist
            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Bookings_UserId' AND object_id = OBJECT_ID(N'[Bookings]'))
                BEGIN
                    DROP INDEX [IX_Bookings_UserId] ON [Bookings];
                END
            ");

            migrationBuilder.Sql(@"
                IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = N'IX_Accommodations_UserId' AND object_id = OBJECT_ID(N'[Accommodations]'))
                BEGIN
                    DROP INDEX [IX_Accommodations_UserId] ON [Accommodations];
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_UserId",
                table: "Accommodations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_Users_UserId",
                table: "Accommodations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

