using Microsoft.EntityFrameworkCore;
using StockTradingApp.Models;

namespace StockTradingApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Watchlist> Watchlists { get; set; }
        public DbSet<StockList> StockLists { get; set; }
        public DbSet<UserStock> UserStocks { get; set; }
      
    }
}
