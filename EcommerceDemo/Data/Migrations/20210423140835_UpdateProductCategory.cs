using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceDemo.Data.Migrations
{
    public partial class UpdateProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "large",
                table: "ProductCatagories",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "medium",
                table: "ProductCatagories",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "small",
                table: "ProductCatagories",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "unit",
                table: "ProductCatagories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "large",
                table: "ProductCatagories");

            migrationBuilder.DropColumn(
                name: "medium",
                table: "ProductCatagories");

            migrationBuilder.DropColumn(
                name: "small",
                table: "ProductCatagories");

            migrationBuilder.DropColumn(
                name: "unit",
                table: "ProductCatagories");
        }
    }
}
