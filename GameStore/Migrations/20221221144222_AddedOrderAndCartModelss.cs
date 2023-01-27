using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Migrations
{
    public partial class AddedOrderAndCartModelss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CartModel_ParentId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartModel",
                table: "CartModel");

            migrationBuilder.RenameTable(
                name: "CartModel",
                newName: "Cart");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cart_ParentId",
                table: "Orders",
                column: "ParentId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cart_ParentId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "CartModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartModel",
                table: "CartModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CartModel_ParentId",
                table: "Orders",
                column: "ParentId",
                principalTable: "CartModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
