using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseConnection.Migrations
{
    public partial class GenresTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genre_GenresId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MoviesId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genre",
                table: "Genre");

            migrationBuilder.RenameTable(
                name: "Genre",
                newName: "Genres");

            migrationBuilder.RenameColumn(
                name: "MoviesId",
                table: "GenreMovie",
                newName: "MoviesTestId");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "GenreMovie",
                newName: "GenresTestId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovie_MoviesId",
                table: "GenreMovie",
                newName: "IX_GenreMovie_MoviesTestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genres",
                table: "Genres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genres_GenresTestId",
                table: "GenreMovie",
                column: "GenresTestId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MoviesTestId",
                table: "GenreMovie",
                column: "MoviesTestId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Genres_GenresTestId",
                table: "GenreMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreMovie_Movies_MoviesTestId",
                table: "GenreMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Genres",
                table: "Genres");

            migrationBuilder.RenameTable(
                name: "Genres",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "MoviesTestId",
                table: "GenreMovie",
                newName: "MoviesId");

            migrationBuilder.RenameColumn(
                name: "GenresTestId",
                table: "GenreMovie",
                newName: "GenresId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreMovie_MoviesTestId",
                table: "GenreMovie",
                newName: "IX_GenreMovie_MoviesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Genre",
                table: "Genre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Genre_GenresId",
                table: "GenreMovie",
                column: "GenresId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreMovie_Movies_MoviesId",
                table: "GenreMovie",
                column: "MoviesId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
