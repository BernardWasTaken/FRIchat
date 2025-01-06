using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FRIchat.Migrations
{
    /// <inheritdoc />
    public partial class AddPredmetIdToOdgovor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Uporabnik",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<int>(
                name: "PredmetId",
                table: "Odgovor",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Odgovor_PredmetId",
                table: "Odgovor",
                column: "PredmetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Odgovor_Predmet_PredmetId",
                table: "Odgovor",
                column: "PredmetId",
                principalTable: "Predmet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Odgovor_Predmet_PredmetId",
                table: "Odgovor");

            migrationBuilder.DropIndex(
                name: "IX_Odgovor_PredmetId",
                table: "Odgovor");

            migrationBuilder.DropColumn(
                name: "PredmetId",
                table: "Odgovor");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Uporabnik",
                type: "TEXT",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
