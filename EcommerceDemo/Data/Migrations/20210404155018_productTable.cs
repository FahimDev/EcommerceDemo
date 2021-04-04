using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceDemo.Data.Migrations
{
    public partial class productTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catagory_id = table.Column<int>(nullable: false),
                    product_img = table.Column<string>(nullable: true),
                    product_name = table.Column<string>(nullable: true),
                    company_id = table.Column<int>(nullable: false),
                    product_unit = table.Column<double>(nullable: false),
                    video_url = table.Column<string>(nullable: true),
                    product_description = table.Column<string>(nullable: true),
                    packing_type = table.Column<string>(nullable: true),
                    product_material = table.Column<string>(nullable: true),
                    product_brand = table.Column<string>(nullable: true),
                    product_color = table.Column<string>(nullable: true),
                    minimum_order = table.Column<double>(nullable: false),
                    location_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
