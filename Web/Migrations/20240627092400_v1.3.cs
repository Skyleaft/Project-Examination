using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Exam_ExamId",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Room",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Exam_ExamId",
                table: "Room",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Exam_ExamId",
                table: "Room");

            migrationBuilder.AlterColumn<int>(
                name: "ExamId",
                table: "Room",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Exam_ExamId",
                table: "Room",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id");
        }
    }
}
