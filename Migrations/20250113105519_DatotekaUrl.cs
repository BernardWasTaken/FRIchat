using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FRIchat.Migrations
{
    /// <inheritdoc />
    public partial class DatotekaUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DatotekaUrl",
                table: "Odgovor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DatotekaUrl",
                table: "Odgovor");
        }
    }
}
