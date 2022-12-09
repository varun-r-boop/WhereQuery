using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orderDetails",
                columns: table => new
                {
                    ordersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    appointmentDate = table.Column<DateTime>(type: "date", nullable: false),
                    orderStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderDetails", x => x.ordersId);
                });

            migrationBuilder.CreateTable(
                name: "customerDetails",
                columns: table => new
                {
                    customerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    customerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerDob = table.Column<DateTime>(type: "date", nullable: false),
                    customerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customerOrdersordersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerDetails", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_customerDetails_orderDetails_customerOrdersordersId",
                        column: x => x.customerOrdersordersId,
                        principalTable: "orderDetails",
                        principalColumn: "ordersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "providerDetails",
                columns: table => new
                {
                    providerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    providerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    providerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    providerMobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    providerPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    orderDetailsordersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providerDetails", x => x.providerId);
                    table.ForeignKey(
                        name: "FK_providerDetails_orderDetails_orderDetailsordersId",
                        column: x => x.orderDetailsordersId,
                        principalTable: "orderDetails",
                        principalColumn: "ordersId");
                });

            migrationBuilder.CreateTable(
                name: "providerServices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    providerDetailsproviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    serviceList = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_providerServices", x => x.id);
                    table.ForeignKey(
                        name: "FK_providerServices_providerDetails_providerDetailsproviderId",
                        column: x => x.providerDetailsproviderId,
                        principalTable: "providerDetails",
                        principalColumn: "providerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_customerDetails_customerOrdersordersId",
                table: "customerDetails",
                column: "customerOrdersordersId");

            migrationBuilder.CreateIndex(
                name: "IX_providerDetails_orderDetailsordersId",
                table: "providerDetails",
                column: "orderDetailsordersId");

            migrationBuilder.CreateIndex(
                name: "IX_providerServices_providerDetailsproviderId",
                table: "providerServices",
                column: "providerDetailsproviderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerDetails");

            migrationBuilder.DropTable(
                name: "providerServices");

            migrationBuilder.DropTable(
                name: "providerDetails");

            migrationBuilder.DropTable(
                name: "orderDetails");
        }
    }
}
