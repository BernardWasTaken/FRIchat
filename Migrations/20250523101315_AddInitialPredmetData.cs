using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FRIchat.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialPredmetData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Predmet",
                columns: new[] { "Id", "Ime", "Letnik", "Predavatelj" },
                values: new object[,]
                {
                    { 1, "Programiranje 1", 1, "Janez Demšar" },
                    { 2, "Podatkovne Baze", 1, "Matjaž Kukar" },
                    { 3, "Računalniška Arhitektura", 1, "Robert Rozman" },
                    { 4, "Informacijski sistemi", 2, "Damjan Vavpotič" },
                    { 5, "Razvoj Informacijskih Sistemov", 2, "Alenka Kavčič" },
                    { 6, "Vhodno-Izhodne Naprave", 2, "Robert Rozman" },
                    { 7, "Praksa", 3, "/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Predmet",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
