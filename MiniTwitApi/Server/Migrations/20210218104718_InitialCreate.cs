using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniTwitApi.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PwHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    WhoId = table.Column<int>(type: "INTEGER", nullable: false),
                    WhomId = table.Column<int>(type: "INTEGER", nullable: false),
                    WhoUserUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    WhomUserUserId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => new { x.WhoId, x.WhomId });
                    table.ForeignKey(
                        name: "FK_Gender_Users_WhomUserUserId",
                        column: x => x.WhomUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gender_Users_WhoUserUserId",
                        column: x => x.WhoUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    authorId = table.Column<int>(type: "INTEGER", nullable: false),
                    authorUsername = table.Column<string>(type: "TEXT", nullable: true),
                    text = table.Column<string>(type: "TEXT", nullable: false),
                    pubDate = table.Column<int>(type: "INTEGER", nullable: false),
                    flagged = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.messageId);
                    table.ForeignKey(
                        name: "FK_Message_Users_authorId",
                        column: x => x.authorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gender_WhomUserUserId",
                table: "Gender",
                column: "WhomUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gender_WhoUserUserId",
                table: "Gender",
                column: "WhoUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_authorId",
                table: "Message",
                column: "authorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
