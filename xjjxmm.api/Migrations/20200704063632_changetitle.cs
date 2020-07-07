using Microsoft.EntityFrameworkCore.Migrations;

namespace xjjxmm.api.Migrations
{
    public partial class changetitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titie",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Blog",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "Titie",
                table: "Blog",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: true);
        }
    }
}
