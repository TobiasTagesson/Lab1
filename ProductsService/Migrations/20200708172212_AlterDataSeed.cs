using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductsService.Migrations
{
    public partial class AlterDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("04259d6e-d326-4d40-8936-cd85e688bab4"),
                column: "ImageUrl",
                value: "/images/Dunlop.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2559ce81-66f4-46f4-a924-8254fd188889"),
                column: "ImageUrl",
                value: "/images/Head.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3a4a7aec-8119-4700-bd81-b22f79089ec1"),
                column: "ImageUrl",
                value: "/images/WilsonBlade.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7dec6022-2f61-47cb-8bd0-8bd9e35680dd"),
                column: "ImageUrl",
                value: "/images/Yonex.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"),
                column: "ImageUrl",
                value: "/images/WilsonProstaff.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe9fee5a-5792-49d4-a146-09e6961b1863"),
                column: "ImageUrl",
                value: "/images/Babolat.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("04259d6e-d326-4d40-8936-cd85e688bab4"),
                column: "ImageUrl",
                value: "https://cdni.onedayonly.co.za/catalog/product/1/_/1_7_12.jpg?auto=compress&bg=fff&fit=fill&h=200&w=200");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2559ce81-66f4-46f4-a924-8254fd188889"),
                column: "ImageUrl",
                value: "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=HPRMPR-1.jpg&nw=43");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3a4a7aec-8119-4700-bd81-b22f79089ec1"),
                column: "ImageUrl",
                value: "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=981619-1.jpg&nw=43");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("7dec6022-2f61-47cb-8bd0-8bd9e35680dd"),
                column: "ImageUrl",
                value: "https://www.mistertennis.com/images/1-media/yonex/racket/EZD98_Plus_Blue_B.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("90d6da79-e0e2-4ba8-bf61-2d94d90df801"),
                column: "ImageUrl",
                value: "https://shop.wilson.com/media/catalog/product/cache/38/image/9df78eab33525d08d6e5fb8d27136e95/c/f/cf893c83c0ff231061c2beb3f5a68306228e5d5c_wrt73900u_pro_staff_97_bl_bl_side.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe9fee5a-5792-49d4-a146-09e6961b1863"),
                column: "ImageUrl",
                value: "https://img.tenniswarehouse-europe.com/watermark/rs.php?path=BPAR-1.jpg&nw=43");
        }
    }
}
