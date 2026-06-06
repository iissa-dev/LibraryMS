using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_People_PersonId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_People_PersonId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employees",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                newName: "IX_Employees_PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_People_PersonId",
                table: "Clients",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_People_PersonId",
                table: "Employees",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_People_PersonId",
                table: "Users",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_People_PersonId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_People_PersonId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_People_PersonId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "Employees",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                newName: "IX_Employees_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_People_PersonId",
                table: "Clients",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Users_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_People_PersonId",
                table: "Users",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
