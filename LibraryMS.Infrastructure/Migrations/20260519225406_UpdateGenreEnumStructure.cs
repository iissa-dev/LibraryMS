using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenreEnumStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Genre",
                table: "Books",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}
