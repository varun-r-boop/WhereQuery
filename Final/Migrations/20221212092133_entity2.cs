using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Final.Migrations
{
    /// <inheritdoc />
    public partial class entity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_orderDetails_customerOrdersordersId",
                table: "customer");

            migrationBuilder.DropIndex(
                name: "IX_customer_customerOrdersordersId",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "customerOrdersordersId",
                table: "customer");

            migrationBuilder.AddColumn<int>(
                name: "orderDetailsordersId",
                table: "customer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_customer_orderDetailsordersId",
                table: "customer",
                column: "orderDetailsordersId");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_orderDetails_orderDetailsordersId",
                table: "customer",
                column: "orderDetailsordersId",
                principalTable: "orderDetails",
                principalColumn: "ordersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_customer_orderDetails_orderDetailsordersId",
                table: "customer");

            migrationBuilder.DropIndex(
                name: "IX_customer_orderDetailsordersId",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "orderDetailsordersId",
                table: "customer");

            migrationBuilder.AddColumn<int>(
                name: "customerOrdersordersId",
                table: "customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_customer_customerOrdersordersId",
                table: "customer",
                column: "customerOrdersordersId");

            migrationBuilder.AddForeignKey(
                name: "FK_customer_orderDetails_customerOrdersordersId",
                table: "customer",
                column: "customerOrdersordersId",
                principalTable: "orderDetails",
                principalColumn: "ordersId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
