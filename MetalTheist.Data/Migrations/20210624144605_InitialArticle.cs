using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MetalTheist.Migrations
{
    public partial class InitialArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "ArticleId", "AuthorId", "Content", "Moniker", "ShortContent", "StatisticsId", "Title", "UploadDate", "UserId" },
                values: new object[] { 1, null, null, "Hello your computer has virus!", "HEL", "Hello virus!", null, "Hello", new DateTime(2021, 6, 24, 17, 46, 4, 600, DateTimeKind.Local).AddTicks(7309), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
