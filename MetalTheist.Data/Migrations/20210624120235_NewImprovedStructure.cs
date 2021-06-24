using Microsoft.EntityFrameworkCore.Migrations;

namespace MetalTheist.Migrations
{
    public partial class NewImprovedStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatisticsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatisticsId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ArticleStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorStatistics", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_StatisticsId",
                table: "Users",
                column: "StatisticsId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_StatisticsId",
                table: "Articles",
                column: "StatisticsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ArticleStatistics_StatisticsId",
                table: "Articles",
                column: "StatisticsId",
                principalTable: "ArticleStatistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AuthorStatistics_StatisticsId",
                table: "Users",
                column: "StatisticsId",
                principalTable: "AuthorStatistics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ArticleStatistics_StatisticsId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AuthorStatistics_StatisticsId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ArticleStatistics");

            migrationBuilder.DropTable(
                name: "AuthorStatistics");

            migrationBuilder.DropIndex(
                name: "IX_Users_StatisticsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Articles_StatisticsId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "StatisticsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StatisticsId",
                table: "Articles");
        }
    }
}
