using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceDemo.Data.Migrations
{
    public partial class addBannerToProductCatagories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "banner_img_path",
                table: "ProductCatagories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "banner_img_path",
                table: "ProductCatagories");
        }
    }
}
