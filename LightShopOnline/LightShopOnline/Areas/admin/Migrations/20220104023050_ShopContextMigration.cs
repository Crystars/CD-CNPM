using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LightShopOnline.Areas.admin.Migrations
{
    public partial class ShopContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Category_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(maxLength: 255, nullable: true),
                    url = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    parentId = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    isHidden = table.Column<int>(nullable: false),
                    Picture1 = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    Coupon_Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Detail = table.Column<string>(nullable: true),
                    Calculator = table.Column<double>(nullable: false),
                    NumberForUsed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.Coupon_Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Product_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(maxLength: 510, nullable: true),
                    url = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    Price = table.Column<long>(nullable: true),
                    Warrant = table.Column<string>(maxLength: 50, nullable: true),
                    Size = table.Column<string>(maxLength: 50, nullable: true),
                    Color = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(maxLength: 50, nullable: true),
                    Discount = table.Column<double>(nullable: true),
                    isHidden = table.Column<int>(nullable: false),
                    Picture1 = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Product_Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    User_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    ggAuthId = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Role = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    Gmail = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Category_Product",
                columns: table => new
                {
                    Product_Id = table.Column<int>(nullable: false),
                    Category_Id = table.Column<int>(nullable: false),
                    Position = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category_Product", x => new { x.Category_Id, x.Product_Id });
                    table.ForeignKey(
                        name: "FK_Category_Product_Category_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "Category",
                        principalColumn: "Category_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Category_Product_Product_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Product",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Cart_Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    User_Id = table.Column<int>(nullable: false),
                    UserTableUser_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Cart_Id);
                    table.ForeignKey(
                        name: "FK_Cart_UserTable_UserTableUser_Id",
                        column: x => x.UserTableUser_Id,
                        principalTable: "UserTable",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Order_Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Guest_Name = table.Column<string>(maxLength: 255, nullable: true),
                    Guest_Phone = table.Column<string>(unicode: false, maxLength: 255, nullable: true),
                    dateCreate = table.Column<DateTime>(nullable: true),
                    Address = table.Column<string>(maxLength: 510, nullable: true),
                    Price = table.Column<long>(nullable: true),
                    Coupon_Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    paymentMethod = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Status = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    User_Id = table.Column<int>(nullable: false),
                    Coupon_Id1 = table.Column<string>(nullable: true),
                    UserTableUser_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Order_Id);
                    table.ForeignKey(
                        name: "FK_Order_Coupon_Coupon_Id1",
                        column: x => x.Coupon_Id1,
                        principalTable: "Coupon",
                        principalColumn: "Coupon_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Order_UserTable_UserTableUser_Id",
                        column: x => x.UserTableUser_Id,
                        principalTable: "UserTable",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Product_Id = table.Column<int>(nullable: false),
                    Order_Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Cart_Id = table.Column<string>(unicode: false, maxLength: 20, nullable: false),
                    Product_Id1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Product_Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Cart_Cart_Id",
                        column: x => x.Cart_Id,
                        principalTable: "Cart",
                        principalColumn: "Cart_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Order",
                        principalColumn: "Order_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Product_Product_Id1",
                        column: x => x.Product_Id1,
                        principalTable: "Product",
                        principalColumn: "Product_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_UserTableUser_Id",
                table: "Cart",
                column: "UserTableUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Product_Product_Id",
                table: "Category_Product",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Coupon_Id1",
                table: "Order",
                column: "Coupon_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserTableUser_Id",
                table: "Order",
                column: "UserTableUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Cart_Id",
                table: "OrderDetail",
                column: "Cart_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Order_Id",
                table: "OrderDetail",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_Product_Id1",
                table: "OrderDetail",
                column: "Product_Id1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category_Product");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
