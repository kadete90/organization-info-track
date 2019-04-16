using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoTrack.DAL.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    Keyword = table.Column<string>(nullable: false),
                    SearchDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchMatch",
                columns: table => new
                {
                    SearchHistoryId = table.Column<Guid>(nullable: false),
                    Entry = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchMatch", x => new { x.SearchHistoryId, x.Entry });
                    table.ForeignKey(
                        name: "FK_SearchMatch_SearchHistories_SearchHistoryId",
                        column: x => x.SearchHistoryId,
                        principalTable: "SearchHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchMatch");

            migrationBuilder.DropTable(
                name: "SearchHistories");
        }
    }
}
