using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingApp.Data;
using StockTradingApp.Models;

namespace StockTradingApp.Data
{
    public class Seed
    {
        private readonly DataContext _context;

        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Users.Any())
            {
                var stocks = new List<Stock>
                {
                    new Stock { StockName = "Apple Inc.", CurrentPrice = 150.25m },
                    new Stock { StockName = "Microsoft Corporation", CurrentPrice = 300.50m },
                    new Stock { StockName = "Amazon.com Inc.", CurrentPrice = 3500.75m },
                    new Stock { StockName = "Google LLC", CurrentPrice = 2800.45m },
                    new Stock { StockName = "Facebook Inc.", CurrentPrice = 320.65m },
                    new Stock { StockName = "Tesla Inc.", CurrentPrice = 750.35m },
                    new Stock { StockName = "Netflix Inc.", CurrentPrice = 500.80m },
                    new Stock { StockName = "NVIDIA Corporation", CurrentPrice = 600.20m },
                    new Stock { StockName = "Adobe Inc.", CurrentPrice = 550.00m },
                    new Stock { StockName = "Intel Corporation", CurrentPrice = 55.30m },
                    new Stock { StockName = "Cisco Systems Inc.", CurrentPrice = 50.25m },
                    new Stock { StockName = "PepsiCo Inc.", CurrentPrice = 140.50m },
                    new Stock { StockName = "Coca-Cola Company", CurrentPrice = 55.75m },
                    new Stock { StockName = "Visa Inc.", CurrentPrice = 220.15m },
                    new Stock { StockName = "Mastercard Incorporated", CurrentPrice = 370.45m },
                    new Stock { StockName = "PayPal Holdings Inc.", CurrentPrice = 250.55m },
                    new Stock { StockName = "IBM Corporation", CurrentPrice = 130.25m },
                    new Stock { StockName = "Sony Corporation", CurrentPrice = 110.75m },
                    new Stock { StockName = "Boeing Company", CurrentPrice = 210.85m },
                    new Stock { StockName = "Disney Walt Co.", CurrentPrice = 190.25m }
                };

                stocks.ForEach(s => _context.Stocks.Add(s));
                _context.SaveChanges();

                // Create some sample users with portfolios and watchlists
                var users = new List<User>
            {
                new User {  Email = "user1@example.com", Password = "user1", Portfolio = new Portfolio(), Watchlist = new Watchlist() },
                new User {  Email = "user2@example.com", Password = "user2", Portfolio = new Portfolio(), Watchlist = new Watchlist() },
                new User {  Email = "user3@example.com", Password = "user3", Portfolio = new Portfolio(), Watchlist = new Watchlist() }
            };
                users.ForEach(u => _context.Users.Add(u));
                _context.SaveChanges();

                // Add some stocks to user portfolios
                var userStocks = new List<UserStock>
                {
                    new UserStock { UserID = 1, StockID = 1, Quantity = 10, PurchasePrice = 148.50m },
                    new UserStock { UserID = 1, StockID = 2, Quantity = 5, PurchasePrice = 295.75m },
                    new UserStock { UserID = 2, StockID = 2, Quantity = 8, PurchasePrice = 305.00m },
                    new UserStock { UserID = 3, StockID = 3, Quantity = 15, PurchasePrice = 3495.25m },
                    new UserStock { UserID = 1, StockID = 4, Quantity = 7, PurchasePrice = 2750.00m },
                    new UserStock { UserID = 1, StockID = 5, Quantity = 20, PurchasePrice = 310.00m },
                    new UserStock { UserID = 2, StockID = 6, Quantity = 4, PurchasePrice = 760.00m },
                    new UserStock { UserID = 3, StockID = 7, Quantity = 9, PurchasePrice = 510.00m },
                    new UserStock { UserID = 1, StockID = 8, Quantity = 6, PurchasePrice = 590.00m },
                    new UserStock { UserID = 2, StockID = 9, Quantity = 12, PurchasePrice = 560.00m },
                    new UserStock { UserID = 3, StockID = 10, Quantity = 25, PurchasePrice = 58.00m },
                    new UserStock { UserID = 1, StockID = 11, Quantity = 30, PurchasePrice = 52.00m },
                    new UserStock { UserID = 2, StockID = 12, Quantity = 15, PurchasePrice = 145.00m },
                    new UserStock { UserID = 3, StockID = 13, Quantity = 18, PurchasePrice = 54.00m },
                    new UserStock { UserID = 1, StockID = 14, Quantity = 11, PurchasePrice = 225.00m },
                    new UserStock { UserID = 2, StockID = 15, Quantity = 7, PurchasePrice = 365.00m },
                    new UserStock { UserID = 3, StockID = 16, Quantity = 5, PurchasePrice = 260.00m },
                    new UserStock { UserID = 1, StockID = 17, Quantity = 13, PurchasePrice = 135.00m },
                    new UserStock { UserID = 2, StockID = 18, Quantity = 8, PurchasePrice = 115.00m },
                    new UserStock { UserID = 3, StockID = 19, Quantity = 6, PurchasePrice = 215.00m },
                    new UserStock { UserID = 1, StockID = 20, Quantity = 10, PurchasePrice = 185.00m }
                };

                userStocks.ForEach(us => _context.UserStocks.Add(us));
                _context.SaveChanges();

                // Add some stocks to watchlists
                users[0].Watchlist.Stocks = new List<Stock> { stocks[0], stocks[1] };
                users[1].Watchlist.Stocks = new List<Stock> { stocks[2] };

                _context.SaveChanges();
            }
        }
    }
}
