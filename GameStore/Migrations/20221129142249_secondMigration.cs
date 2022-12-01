using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Migrations
{
    public partial class secondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GamesAndGenres_GameId",
                table: "GamesAndGenres",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamesAndGenres_GenreId",
                table: "GamesAndGenres",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamesAndGenres_Games_GameId",
                table: "GamesAndGenres",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamesAndGenres_Genres_GenreId",
                table: "GamesAndGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "GenreId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamesAndGenres_Games_GameId",
                table: "GamesAndGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GamesAndGenres_Genres_GenreId",
                table: "GamesAndGenres");

            migrationBuilder.DropIndex(
                name: "IX_GamesAndGenres_GameId",
                table: "GamesAndGenres");

            migrationBuilder.DropIndex(
                name: "IX_GamesAndGenres_GenreId",
                table: "GamesAndGenres");
        }
    }
}
