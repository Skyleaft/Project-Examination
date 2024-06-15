using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "FK_UserAnswer_Soal_SoalId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_AspNetUsers_UserId",
                table: "UserExam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_Room_RoomId",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "Gambar",
                table: "Soal");

            migrationBuilder.DropColumn(
                name: "KunciJawaban",
                table: "Soal");

            migrationBuilder.DropColumn(
                name: "Pilihan",
                table: "Soal");

            migrationBuilder.RenameColumn(
                name: "Point",
                table: "Soal",
                newName: "BobotPoint");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserExam",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "UserExam",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "UserExam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "UserExam",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ScoreNormalize",
                table: "UserExam",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimeLeft",
                table: "UserExam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SoalId",
                table: "UserAnswer",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SoalJawabanId",
                table: "UserAnswer",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Thumbnail",
                table: "Exam",
                type: "bytea",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.CreateTable(
                name: "SoalJawaban",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SoalId = table.Column<int>(type: "integer", nullable: false),
                    Jawaban = table.Column<string>(type: "text", nullable: false),
                    IsBenar = table.Column<bool>(type: "boolean", nullable: false),
                    Point = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoalJawaban", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoalJawaban_Soal_SoalId",
                        column: x => x.SoalId,
                        principalTable: "Soal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserExam_ExamId",
                table: "UserExam",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_SoalJawabanId",
                table: "UserAnswer",
                column: "SoalJawabanId");

            migrationBuilder.CreateIndex(
                name: "IX_SoalJawaban_SoalId",
                table: "SoalJawaban",
                column: "SoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_SoalJawaban_SoalJawabanId",
                table: "UserAnswer",
                column: "SoalJawabanId",
                principalTable: "SoalJawaban",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Soal_SoalId",
                table: "UserAnswer",
                column: "SoalId",
                principalTable: "Soal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_AspNetUsers_UserId",
                table: "UserExam",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam",
                column: "ExamId",
                principalTable: "Exam",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_Room_RoomId",
                table: "UserExam",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_SoalJawaban_SoalJawabanId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Soal_SoalId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_AspNetUsers_UserId",
                table: "UserExam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_Exam_ExamId",
                table: "UserExam");

            migrationBuilder.DropForeignKey(
                name: "FK_UserExam_Room_RoomId",
                table: "UserExam");

            migrationBuilder.DropTable(
                name: "SoalJawaban");

            migrationBuilder.DropIndex(
                name: "IX_UserExam_ExamId",
                table: "UserExam");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_SoalJawabanId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "ExamId",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "ScoreNormalize",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "TimeLeft",
                table: "UserExam");

            migrationBuilder.DropColumn(
                name: "SoalJawabanId",
                table: "UserAnswer");

            migrationBuilder.RenameColumn(
                name: "BobotPoint",
                table: "Soal",
                newName: "Point");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "UserExam",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "RoomId",
                table: "UserExam",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SoalId",
                table: "UserAnswer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "UserAnswer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Gambar",
                table: "Soal",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KunciJawaban",
                table: "Soal",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<List<string>>(
                name: "Pilihan",
                table: "Soal",
                type: "text[]",
                nullable: false);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Thumbnail",
                table: "Exam",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Soal_SoalId",
                table: "UserAnswer",
                column: "SoalId",
                principalTable: "Soal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_AspNetUsers_UserId",
                table: "UserExam",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserExam_Room_RoomId",
                table: "UserExam",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");
        }
    }
}
