using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceDemo.Data.Migrations
{
    public partial class updateProductImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image_path",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "image1_path",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image2_path",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image3_path",
                table: "ProductImages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image4_path",
                table: "ProductImages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image1_path",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "image2_path",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "image3_path",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "image4_path",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "image_path",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
