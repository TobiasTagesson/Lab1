using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsService.Migrations
{
    public partial class Initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"), "Racquet for the advanced player", "https://shop.wilson.com/media/catalog/product/cache/38/image/9df78eab33525d08d6e5fb8d27136e95/c/f/cf893c83c0ff231061c2beb3f5a68306228e5d5c_wrt73900u_pro_staff_97_bl_bl_side.jpg", "Wilson Pro Staff 97", 199.00m },
                    { new Guid("04259d6e-d326-4d40-8936-cd85e688bab4"), "Racquet with great control", "https://cdni.onedayonly.co.za/catalog/product/1/_/1_7_12.jpg?auto=compress&bg=fff&fit=fill&h=200&w=200", "Dunlop Biomimetic 300", 179.00m },
                    { new Guid("7dec6022-2f61-47cb-8bd0-8bd9e35680dd"), "Great power, great control", "https://www.mistertennis.com/images/1-media/yonex/racket/EZD98_Plus_Blue_B.jpg", "Yonex DR 98", 189.00m },
                    { new Guid("2559ce81-66f4-46f4-a924-8254fd188889"), "Classic control racquet", "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=HPRMPR-1.jpg&nw=43", "Head Prestige MP", 209.00m },
                    { new Guid("3a4a7aec-8119-4700-bd81-b22f79089ec1"), "Modern feel", "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=981619-1.jpg&nw=43", "Wilson Blade 98", 199.00m },
                    { new Guid("fe9fee5a-5792-49d4-a146-09e6961b1863"), "Spin friendly racquet", "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=BPAR-1.jpg&nw=43", "Babolat Pure Aero", 169.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
