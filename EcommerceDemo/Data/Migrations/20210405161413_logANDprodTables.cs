using Microsoft.EntityFrameworkCore.Migrations;

namespace EcommerceDemo.Data.Migrations
{
    public partial class logANDprodTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Login",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(nullable: false),
                    username = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Login", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catagory_id = table.Column<int>(nullable: false),
                    product_name = table.Column<string>(nullable: true),
                    company_id = table.Column<int>(nullable: false),
                    product_img = table.Column<string>(nullable: true),
                    video_url = table.Column<string>(nullable: true),
                    product_description = table.Column<string>(nullable: true),
                    packing_type = table.Column<string>(nullable: true),
                    product_material = table.Column<string>(nullable: true),
                    product_brand = table.Column<string>(nullable: true),
                    product_color = table.Column<string>(nullable: true),
                    minimum_order = table.Column<string>(nullable: true),
                    product_sell = table.Column<string>(nullable: true),
                    product_price = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Login");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
