using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketMatrix_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_MyCart_ProductId",
            table: "MyCart");

            // Create a composite unique index on UserId + ProductId
            migrationBuilder.CreateIndex(
                name: "IX_MyCart_UserId_ProductId",
                table: "MyCart",
                columns: new[] { "UserId", "ProductId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert changes (in case of rollback)
            migrationBuilder.DropIndex(
                name: "IX_MyCart_UserId_ProductId",
                table: "MyCart");

            migrationBuilder.CreateIndex(
                name: "IX_MyCart_ProductId",
                table: "MyCart",
                column: "ProductId",
                unique: true);
        }
    }
}
