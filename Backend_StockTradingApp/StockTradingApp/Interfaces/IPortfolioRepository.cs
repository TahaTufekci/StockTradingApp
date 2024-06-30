using StockTradingApp.Models;
using System.Collections.Generic;

namespace StockTradingApp.Interfaces
{
    public interface IPortfolioRepository
    {
        Portfolio GetPortfolio(int userId); // New method
        void AddPortfolio(Portfolio portfolio);
        void UpdatePortfolio(Portfolio portfolio); // New method
        void DeletePortfolio(int portfolioId);
        void AddUserStockToPortfolio(int portfolioId, int userId, int userStockId, int quantity);
        UserStock GetUserStock(int userId, int stockId);
        void UpdateUserStock(UserStock userStock);
    }
}
