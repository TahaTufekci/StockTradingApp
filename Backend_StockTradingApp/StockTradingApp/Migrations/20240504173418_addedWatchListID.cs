using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTradingApp.Migrations
{
    /// <inheritdoc />
    public partial class addedWatchListID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Watchlists_WatchlistUserID",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlists",
                table: "Watchlists");

            migrationBuilder.RenameColumn(
                name: "WatchlistUserID",
                table: "Stocks",
                newName: "WatchlistID");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_WatchlistUserID",
                table: "Stocks",
                newName: "IX_Stocks_WatchlistID");

            migrationBuilder.AddColumn<int>(
                name: "WatchlistID",
                table: "Watchlists",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlists",
                table: "Watchlists",
                column: "WatchlistID");

            migrationBuilder.CreateIndex(
                name: "IX_Watchlists_UserID",
                table: "Watchlists",
                column: "UserID",
                unique: true);

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Watchlists",
                table: "Watchlists");

            migrationBuilder.DropIndex(
                name: "IX_Watchlists_UserID",
                table: "Watchlists");

            migrationBuilder.DropColumn(
                name: "WatchlistID",
                table: "Watchlists");

            migrationBuilder.RenameColumn(
                name: "WatchlistID",
                table: "Stocks",
                newName: "WatchlistUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_WatchlistID",
                table: "Stocks",
                newName: "IX_Stocks_WatchlistUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Watchlists",
                table: "Watchlists",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Watchlists_WatchlistUserID",
                table: "Stocks",
                column: "WatchlistUserID",
                principalTable: "Watchlists",
                principalColumn: "UserID");
        }
    }
}
