using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class v12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Jadwal",
                table: "Room",
                newName: "JadwalStart");

            migrationBuilder.AddColumn<byte[]>(
                name: "Gambar",
                table: "Soal",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Soal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Durasi",
                table: "Room",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "JadwalEnd",
                table: "Room",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Exam",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Exam",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Exam",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedOn",
                table: "Exam",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nama",
                table: "Exam",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Exam",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumbnail",
                table: "Exam",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "TotalPoint",
                table: "Exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Room_RoomId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_RoomId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "Gambar",
                table: "Soal");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "Soal");

            migrationBuilder.DropColumn(
                name: "Durasi",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "JadwalEnd",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "LastModifiedOn",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "Nama",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "TotalPoint",
                table: "Exam");

            migrationBuilder.RenameColumn(
                name: "JadwalStart",
                table: "Room",
                newName: "Jadwal");

            migrationBuilder.AddColumn<int>(
                name: "ExamId",
                table: "Room",
                type: "integer",
                nullable: true);

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
    }
}
