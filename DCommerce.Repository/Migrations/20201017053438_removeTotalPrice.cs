using Microsoft.EntityFrameworkCore.Migrations;

namespace DCommerce.Repository.Migrations
{
    public partial class removeTotalPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "CartItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalPrice",
                table: "CartItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
