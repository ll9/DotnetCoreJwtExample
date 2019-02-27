using Microsoft.EntityFrameworkCore.Migrations;

namespace JwtAuthAPiCore.Migrations
{
    public partial class JwtToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JwtTokens",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Revoked = table.Column<bool>(nullable: false),
                    MobileUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JwtTokens_MobileUsers_MobileUserId",
                        column: x => x.MobileUserId,
                        principalTable: "MobileUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JwtTokens_MobileUserId",
                table: "JwtTokens",
                column: "MobileUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JwtTokens");
        }
    }
}
