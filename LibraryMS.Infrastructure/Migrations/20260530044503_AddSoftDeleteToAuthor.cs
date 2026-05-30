using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Authors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Authors");
        }
    }
}
