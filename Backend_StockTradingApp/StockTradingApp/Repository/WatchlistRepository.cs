using Microsoft.EntityFrameworkCore;
using StockTradingApp.Data;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System;
using System.Linq;

namespace StockTradingApp.Repository
{
    public class WatchlistRepository : IWatchlistRepository
    {
        private readonly DataContext _context;

        public WatchlistRepository(DataContext context)
        {
            _context = context;
        }

        public Watchlist GetUserWatchlist(int userId)
        {
            return _context.Watchlists.Include(w => w.Stocks)
                .FirstOrDefault(w => w.UserID.Equals(userId)); ;
        }

        public void AddWatchlist(Watchlist watchlist)
        {
            // Add the watchlist to the database context
            _context.Watchlists.Add(watchlist);

            // Save changes to the database
            _context.SaveChanges();
        }

        public void UpdateWatchlist(Watchlist watchlist)
        {
            // Update the watchlist entity in the database context
            _context.Watchlists.Update(watchlist);

            // Save changes to the database
            _context.SaveChanges();
        }
        public void AddStockToWatchlist(int stockId, int userID)
        {
            var existingWatchlist = _context.Watchlists
                .FirstOrDefault(w => w.UserID == userID);


            if (existingWatchlist == null)
            {
                // If the watchlist item doesn't exist, create a new one
                existingWatchlist = new Watchlist
                {
                    UserID = userID,
                    Stocks = new List<Stock> {  }
                };
                _context.Watchlists.Add(existingWatchlist);
            }
            else
            {
                var stockData = _context.Stocks
                  .AsNoTracking()
                  .Select(s => new { StockID = s.StockID, StockName = s.StockName, s.CurrentPrice })
                  .ToList() // Materialize results into a list (optional)
                  .FirstOrDefault(s => s.StockID == stockId);
                // ... (handle case where stockData is null)

                if (stockData != null)
                {
                    // Add the data to the watchlist (creating a new instance)
                    existingWatchlist.Stocks.Add(new Stock { StockID = stockData.StockID, StockName = stockData.StockName, CurrentPrice = stockData.CurrentPrice });
                }
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw; // Re-throw the exception for handling in the controller
            }
        }

        public void RemoveStockFromWatchlist(int userId, int stockId)
        {
            var watchlist = _context.Watchlists
                .Include(w => w.Stocks)
                .FirstOrDefault(w => w.UserID == userId);

            if (watchlist != null)
            {
                var stockToRemove = watchlist.Stocks.FirstOrDefault(s => s.StockID == stockId);
                if (stockToRemove != null)
                {
                    watchlist.Stocks.Remove(stockToRemove);
                    _context.SaveChanges();
                    return;
                }
            }
            throw new InvalidOperationException("Stock is not in the user's watchlist.");
        }

        public bool IsStockInWatchlist(int userId, int stockId)
        {
           return _context.Watchlists
                .Any(w => w.UserID == userId && w.Stocks.Any(s => s.StockID == stockId));
        }
    }
}
