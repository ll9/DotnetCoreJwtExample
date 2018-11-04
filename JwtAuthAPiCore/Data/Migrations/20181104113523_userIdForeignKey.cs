using Microsoft.EntityFrameworkCore.Migrations;

namespace JwtAuthAPiCore.Data.Migrations
{
    public partial class userIdForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "MapData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MapData");
        }
    }
}
