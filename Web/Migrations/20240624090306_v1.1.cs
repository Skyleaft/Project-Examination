using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class v11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BobotPoint",
                table: "Soal");

            migrationBuilder.AddColumn<bool>(
                name: "isMultipleJawaban",
                table: "Soal",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMultipleJawaban",
                table: "Soal");

            migrationBuilder.AddColumn<int>(
                name: "BobotPoint",
                table: "Soal",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
