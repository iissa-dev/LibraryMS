using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCopyIdToReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCopyId",
                table: "Reservations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_BookCopyId",
                table: "Reservations",
                column: "BookCopyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_BookCopies_BookCopyId",
                table: "Reservations",
                column: "BookCopyId",
                principalTable: "BookCopies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_BookCopies_BookCopyId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_BookCopyId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "BookCopyId",
                table: "Reservations");
        }
    }
}
