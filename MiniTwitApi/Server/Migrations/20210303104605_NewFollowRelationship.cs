using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniTwitApi.Server.Migrations
{
    public partial class NewFollowRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_UserId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Followers_WhomId",
                table: "Followers",
                column: "WhomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Followers_Users_WhoId",
                table: "Followers",
                column: "WhoId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Followers_Users_WhomId",
                table: "Followers",
                column: "WhomId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Followers_Users_WhoId",
                table: "Followers");

            migrationBuilder.DropForeignKey(
                name: "FK_Followers_Users_WhomId",
                table: "Followers");

            migrationBuilder.DropIndex(
                name: "IX_Followers_WhomId",
                table: "Followers");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserId1",
                table: "Users",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_UserId1",
                table: "Users",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
