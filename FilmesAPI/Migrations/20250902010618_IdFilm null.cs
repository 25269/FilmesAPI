using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmsAPI.Migrations
{
    /// <inheritdoc />
    public partial class IdFilmnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Films_IdFilm",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "IdFilm",
                table: "Sessions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Films_IdFilm",
                table: "Sessions",
                column: "IdFilm",
                principalTable: "Films",
                principalColumn: "IdFilm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Films_IdFilm",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "IdFilm",
                table: "Sessions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Films_IdFilm",
                table: "Sessions",
                column: "IdFilm",
                principalTable: "Films",
                principalColumn: "IdFilm",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
