using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class v14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Room_RoomId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam");

            migrationBuilder.DropIndex(
                name: "IX_UserExam_ExamId",
                table: "UserExam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_RoomId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Exam");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Room",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPeserta",
                table: "Room",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Room_ExamId",
                table: "Room",
                column: "ExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Exam_ExamId",
                table: "Room",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Exam_ExamId",
                table: "Room");

            migrationBuilder.DropIndex(
                name: "IX_Room_ExamId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "TotalPeserta",
                table: "Room");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "UserExam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Exam",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserExam_ExamId",
                table: "UserExam",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_RoomId",
                table: "Exam",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Room_RoomId",
                table: "Exam",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
