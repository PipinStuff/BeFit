using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imię",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nazwisko",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SesjeTreningowe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataRozpoczęcia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataZakończenia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUżytkownika = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UżytkownikId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesjeTreningowe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesjeTreningowe_AspNetUsers_UżytkownikId",
                        column: x => x.UżytkownikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TypyĆwiczeń",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nazwa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypyĆwiczeń", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WykonaneĆwiczenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSesjiTreningowej = table.Column<int>(type: "int", nullable: false),
                    IdTypuĆwiczenia = table.Column<int>(type: "int", nullable: false),
                    Obciążenie = table.Column<double>(type: "float", nullable: false),
                    LiczbaSerii = table.Column<int>(type: "int", nullable: false),
                    Powtórzenia = table.Column<int>(type: "int", nullable: false),
                    DataWykonania = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SesjaTreningowaId = table.Column<int>(type: "int", nullable: false),
                    TypĆwiczeniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WykonaneĆwiczenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WykonaneĆwiczenia_SesjeTreningowe_SesjaTreningowaId",
                        column: x => x.SesjaTreningowaId,
                        principalTable: "SesjeTreningowe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WykonaneĆwiczenia_TypyĆwiczeń_TypĆwiczeniaId",
                        column: x => x.TypĆwiczeniaId,
                        principalTable: "TypyĆwiczeń",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SesjeTreningowe_UżytkownikId",
                table: "SesjeTreningowe",
                column: "UżytkownikId");

            migrationBuilder.CreateIndex(
                name: "IX_WykonaneĆwiczenia_SesjaTreningowaId",
                table: "WykonaneĆwiczenia",
                column: "SesjaTreningowaId");

            migrationBuilder.CreateIndex(
                name: "IX_WykonaneĆwiczenia_TypĆwiczeniaId",
                table: "WykonaneĆwiczenia",
                column: "TypĆwiczeniaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WykonaneĆwiczenia");

            migrationBuilder.DropTable(
                name: "SesjeTreningowe");

            migrationBuilder.DropTable(
                name: "TypyĆwiczeń");

            migrationBuilder.DropColumn(
                name: "Imię",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nazwisko",
                table: "AspNetUsers");
        }
    }
}
