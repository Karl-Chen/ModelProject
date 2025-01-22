using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandID = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "Ordertatus",
                columns: table => new
                {
                    OrdertatusID = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    OrdertatusName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordertatus", x => x.OrdertatusID);
                });

            migrationBuilder.CreateTable(
                name: "PayWay",
                columns: table => new
                {
                    PayWayID = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    PayWayName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayWay", x => x.PayWayID);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecification",
                columns: table => new
                {
                    SpecificationID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SpecificationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecification", x => x.SpecificationID);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    TypeID = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingWay",
                columns: table => new
                {
                    ShippingWayID = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    ShippingWayName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingWay", x => x.ShippingWayID);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierID = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SupplierName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactWindow = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Moblie = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Addr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierID);
                });

            migrationBuilder.CreateTable(
                name: "MemberAcc",
                columns: table => new
                {
                    Account = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberID = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberAcc", x => x.Account);
                    table.ForeignKey(
                        name: "FK_MemberAcc_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberTel",
                columns: table => new
                {
                    Index = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TelNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MemberID = table.Column<string>(type: "nvarchar(8)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTel", x => x.Index);
                    table.ForeignKey(
                        name: "FK_MemberTel_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    RoleID = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staff_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderNo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsGoodPackage = table.Column<bool>(type: "bit", nullable: false),
                    ShippingAddr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PayWayID = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    OrdertatusID = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    MemberID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ShippingWayID = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderNo);
                    table.ForeignKey(
                        name: "FK_Order_Member_MemberID",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Ordertatus_OrdertatusID",
                        column: x => x.OrdertatusID,
                        principalTable: "Ordertatus",
                        principalColumn: "OrdertatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_PayWay_PayWayID",
                        column: x => x.PayWayID,
                        principalTable: "PayWay",
                        principalColumn: "PayWayID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_ShippingWay_ShippingWayID",
                        column: x => x.ShippingWayID,
                        principalTable: "ShippingWay",
                        principalColumn: "ShippingWayID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prodect",
                columns: table => new
                {
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    CostJP = table.Column<int>(type: "int", nullable: false),
                    CostExchangeRate = table.Column<float>(type: "real", nullable: false),
                    PriceExchangeRage = table.Column<float>(type: "real", nullable: false),
                    Inventory = table.Column<int>(type: "int", nullable: false),
                    OrderedQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductTypeID = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    ProductSpecificationID = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    BrandID = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    SupplierID = table.Column<string>(type: "nvarchar(2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodect", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Prodect_Brand_BrandID",
                        column: x => x.BrandID,
                        principalTable: "Brand",
                        principalColumn: "BrandID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prodect_ProductSpecification_ProductSpecificationID",
                        column: x => x.ProductSpecificationID,
                        principalTable: "ProductSpecification",
                        principalColumn: "SpecificationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prodect_ProductType_ProductTypeID",
                        column: x => x.ProductTypeID,
                        principalTable: "ProductType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prodect_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "SupplierID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HandleOrder",
                columns: table => new
                {
                    StaffID = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    HandleTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandleOrder", x => new { x.StaffID, x.OrderNo });
                    table.ForeignKey(
                        name: "FK_HandleOrder_Order_OrderNo",
                        column: x => x.OrderNo,
                        principalTable: "Order",
                        principalColumn: "OrderNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HandleOrder_Staff_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceNo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    OrderNo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceNo);
                    table.ForeignKey(
                        name: "FK_Invoice_Order_OrderNo",
                        column: x => x.OrderNo,
                        principalTable: "Order",
                        principalColumn: "OrderNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    OrderNo = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    ProductID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Off = table.Column<float>(type: "real", nullable: false),
                    Vol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => new { x.OrderNo, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderDetail_Order_OrderNo",
                        column: x => x.OrderNo,
                        principalTable: "Order",
                        principalColumn: "OrderNo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Prodect_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Prodect",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HandleOrder_OrderNo",
                table: "HandleOrder",
                column: "OrderNo");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_OrderNo",
                table: "Invoice",
                column: "OrderNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberAcc_MemberID",
                table: "MemberAcc",
                column: "MemberID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MemberTel_MemberID",
                table: "MemberTel",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MemberID",
                table: "Order",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrdertatusID",
                table: "Order",
                column: "OrdertatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PayWayID",
                table: "Order",
                column: "PayWayID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_ShippingWayID",
                table: "Order",
                column: "ShippingWayID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_ProductID",
                table: "OrderDetail",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Prodect_BrandID",
                table: "Prodect",
                column: "BrandID");

            migrationBuilder.CreateIndex(
                name: "IX_Prodect_ProductSpecificationID",
                table: "Prodect",
                column: "ProductSpecificationID");

            migrationBuilder.CreateIndex(
                name: "IX_Prodect_ProductTypeID",
                table: "Prodect",
                column: "ProductTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Prodect_SupplierID",
                table: "Prodect",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_RoleID",
                table: "Staff",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HandleOrder");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "MemberAcc");

            migrationBuilder.DropTable(
                name: "MemberTel");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Prodect");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Ordertatus");

            migrationBuilder.DropTable(
                name: "PayWay");

            migrationBuilder.DropTable(
                name: "ShippingWay");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "ProductSpecification");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
