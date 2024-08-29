using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IudexBoost.Migrations
{
    /// <inheritdoc />
    public partial class alterordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Orders",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Orders",
                newName: "Amount");
        }
    }
}
