using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Role_RoleId",
                table: "UserProfile");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserProfile",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Role_RoleId",
                table: "UserProfile",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Role_RoleId",
                table: "UserProfile");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "UserProfile",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Role_RoleId",
                table: "UserProfile",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id");
        }
    }
}
