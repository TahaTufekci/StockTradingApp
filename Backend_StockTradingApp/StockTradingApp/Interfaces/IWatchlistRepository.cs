using StockTradingApp.Models;
using System.Collections.Generic;

namespace StockTradingApp.Interfaces
{
    public interface IWatchlistRepository
    {
        Watchlist GetUserWatchlist(int userId);
        void AddStockToWatchlist(int stockId, int userID);
        void RemoveStockFromWatchlist(int userId, int stockId);
        bool IsStockInWatchlist(int userId, int stockId);
        void AddWatchlist(Watchlist watchlist);
        void UpdateWatchlist(Watchlist watchlist);
    }
}
