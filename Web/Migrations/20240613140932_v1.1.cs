using System;
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
                name: "TangalLahir",
                table: "AspNetUsers");

            migrationBuilder.EnsureSchema(
                name: "reference");

            migrationBuilder.AddColumn<string>(
                name: "KotaId",
                table: "AspNetUsers",
                type: "character varying(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pekerjaan",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Provinsi",
                schema: "reference",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NamaProvinsi = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinsi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kota",
                schema: "reference",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProvinsiId = table.Column<string>(type: "character varying(100)", nullable: false),
                    NamaKota = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kota", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kota_Provinsi_ProvinsiId",
                        column: x => x.ProvinsiId,
                        principalSchema: "reference",
                        principalTable: "Provinsi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KotaId",
                table: "AspNetUsers",
                column: "KotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Kota_ProvinsiId",
                schema: "reference",
                table: "Kota",
                column: "ProvinsiId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Kota_KotaId",
                table: "AspNetUsers",
                column: "KotaId",
                principalSchema: "reference",
                principalTable: "Kota",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Kota_KotaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Kota",
                schema: "reference");

            migrationBuilder.DropTable(
                name: "Provinsi",
                schema: "reference");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_KotaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KotaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Pekerjaan",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "TangalLahir",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}
