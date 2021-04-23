using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceDemo.Data.Migrations
{
    public partial class removeVolumnIDFromProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_volume_id",
                table: "ProductCatagories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "product_volume_id",
                table: "ProductCatagories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
