using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApiP223.Data.Migrations
{
    public partial class DisplayStatusAddedIntoProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DisplayStatus",
                table: "Products",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayStatus",
                table: "Products");
        }
    }
}
