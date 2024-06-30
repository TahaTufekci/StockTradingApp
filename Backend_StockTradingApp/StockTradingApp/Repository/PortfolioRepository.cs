using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StockTradingApp.Data;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;

public class PortfolioRepository : IPortfolioRepository
{
    private readonly DataContext _context;

    public PortfolioRepository(DataContext context)
    {
        _context = context;
    }

    public Portfolio GetPortfolio(int userId)
    {
        return _context.Portfolios.Include(s => s.UserStocks).ThenInclude(t => t.Stock).FirstOrDefault(p => p.UserID == userId);
    }

    public void AddPortfolio(Portfolio portfolio)
    {
        _context.Portfolios.Add(portfolio);
        _context.SaveChanges();
    }

    public void UpdatePortfolio(Portfolio portfolio)
    {
        _context.Portfolios.Update(portfolio);
        _context.SaveChanges();
    }

    public void DeletePortfolio(int portfolioId)
    {
        var portfolio = _context.Portfolios.Find(portfolioId);
        if (portfolio != null)
        {
            _context.Portfolios.Remove(portfolio);
            _context.SaveChanges();
        }
    }

    public void AddUserStockToPortfolio(int portfolioId,int userId, int userStockId, int quantity)
    {
        // Check if portfolio exists
        var portfolio = _context.Portfolios.Find(portfolioId);
        if (portfolio == null)
        {
            throw new ArgumentException("Portfolio with the provided ID does not exist.");
        }

        // Check if userStock already exists in the portfolio
        if (portfolio.UserStocks.Any(us => us.StockID == userStockId))
        {
            portfolio.UserStocks.FirstOrDefault(us => us.StockID == userStockId).Quantity += quantity;
        }
        else
        {
            // Create a new Portfolio item
            var userStock = new UserStock
            {
                UserID = userId,
                StockID = userStockId,
                Quantity = quantity,
                PurchasePrice = _context.UserStocks.FirstOrDefault(us => us.StockID == userStockId).PurchasePrice
            };
            // Add the userStock to the portfolio's UserStocks collection
            portfolio.UserStocks.Add(userStock);
        }
        // Save changes to the database
        _context.SaveChanges();
    }
    public UserStock GetUserStock(int userId, int stockId)
    {
        var portfolio = _context.Portfolios
      .Include(p => p.UserStocks).ThenInclude(s => s.Stock) // Eagerly load UserStocks collection
      .FirstOrDefault(p => p.UserID == userId);

        // Check if the portfolio exists
        if (portfolio == null)
        {
            return null; // User might not have a portfolio yet
        }

        // Find the user stock within the portfolio
        return portfolio.UserStocks.FirstOrDefault(us => us.StockID == stockId);
    }
    public void UpdateUserStock(UserStock userStock)
    {
        _context.UserStocks.Update(userStock);
        _context.SaveChanges();
    }
}
