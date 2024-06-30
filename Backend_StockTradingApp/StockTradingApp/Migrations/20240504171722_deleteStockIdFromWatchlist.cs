using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTradingApp.Migrations
{
    /// <inheritdoc />
    public partial class deleteStockIdFromWatchlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockID",
                table: "Watchlists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockID",
                table: "Watchlists",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
