using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;

        public DashboardController(IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
        {
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
        }

        // Define the TotalQuantity property as read-write
        public decimal TotalQuantity { get; set; }

        [HttpGet("portfolio/{userId}")]
        public IActionResult GetPortfolio(int userId)
        {
            try
            {
                // Validate user ID
                if (userId <= 0)
                {
                    return BadRequest("Invalid user ID");
                }

                // Retrieve the portfolio using the repository
                var portfolio = _portfolioRepository.GetPortfolio(userId);

                // Check if portfolio exists
                if (portfolio == null)
                {
                    return NotFound("Portfolio not found.");
                }

                // Validate portfolio ownership (optional)
                if (portfolio.UserID != userId)
                {
                    return Unauthorized("Access denied. You cannot view this portfolio.");
                }
                if(portfolio.UserStocks.Count > 0)
                {
                    var stockDetails = portfolio.UserStocks.Select(us => new
                    {
                        StockId = us.StockID,
                        StockName = us.Stock.StockName,
                        Quantity = us.Quantity,
                        PurchasePrice = us.PurchasePrice,
                        CurrentPrice = us.Stock.CurrentPrice,
                        Profit = (us.Stock.CurrentPrice * us.Quantity) - (us.PurchasePrice * us.Quantity),
                        ProfitPercentage = us.Quantity > 0 ? (us.Stock.CurrentPrice - us.PurchasePrice) / us.PurchasePrice * 100 : 0
                    });

                    // Calculate total quantity, total profit, and total percentage of profit
                    decimal totalQuantity = stockDetails.Sum(stock => stock.Quantity);
                    decimal totalProfit = stockDetails.Sum(stock => stock.Profit);
                    decimal totalProfitPercentage = totalProfit / portfolio.UserStocks.Sum(us => us.PurchasePrice * us.Quantity) * 100;

                    // Prepare portfolio details with total values
                    var portfolioDetails = new
                    {
                        TotalProfit = totalProfit,
                        TotalProfitPercentage = totalProfitPercentage,
                        Stocks = stockDetails
                    };

                    return Ok(portfolioDetails);
                }
                else
                {
                    var stockDetails = portfolio.UserStocks.Select(us => new
                    {
                        StockId = 0,
                        StockName = 0,
                        Quantity = 0,
                        PurchasePrice = 0,
                        CurrentPrice = 0,
                        Profit = 0,
                        ProfitPercentage = 0
                    });

                    // Calculate total quantity, total profit, and total percentage of profit
                    decimal totalProfit = 0;
                    decimal totalProfitPercentage = 0;

                    // Prepare portfolio details with total values
                    var portfolioDetails = new
                    {
                        TotalProfit = totalProfit,
                        TotalProfitPercentage = totalProfitPercentage,
                        Stocks = stockDetails
                    };
                    return Ok(portfolioDetails);
                }
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
