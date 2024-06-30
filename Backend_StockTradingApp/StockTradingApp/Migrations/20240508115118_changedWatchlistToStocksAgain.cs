using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTradingApp.Migrations
{
    /// <inheritdoc />
    public partial class changedWatchlistToStocksAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserStocks_Watchlists_WatchlistID",
                table: "UserStocks");

            migrationBuilder.DropIndex(
                name: "IX_UserStocks_WatchlistID",
                table: "UserStocks");

            migrationBuilder.DropColumn(
                name: "WatchlistID",
                table: "UserStocks");

            migrationBuilder.AddColumn<int>(
                name: "WatchlistID",
                table: "Stocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_WatchlistID",
                table: "Stocks",
                column: "WatchlistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Watchlists_WatchlistID",
                table: "Stocks",
                column: "WatchlistID",
                principalTable: "Watchlists",
                principalColumn: "WatchlistID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Watchlists_WatchlistID",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_WatchlistID",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "WatchlistID",
                table: "Stocks");

            migrationBuilder.AddColumn<int>(
                name: "WatchlistID",
                table: "UserStocks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserStocks_WatchlistID",
                table: "UserStocks",
                column: "WatchlistID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserStocks_Watchlists_WatchlistID",
                table: "UserStocks",
                column: "WatchlistID",
                principalTable: "Watchlists",
                principalColumn: "WatchlistID");
        }
    }
}
