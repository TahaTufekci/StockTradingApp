using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System;
using System.Linq;

namespace StockTradingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuySellController : ControllerBase
    {
        private readonly IWatchlistRepository _watchlistRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IStockRepository _stockRepository;

        public BuySellController(IWatchlistRepository watchlistRepository, IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
        {
            _watchlistRepository = watchlistRepository;
            _portfolioRepository = portfolioRepository;
            _stockRepository = stockRepository;
        }

        [HttpPost("buyFromWatchlist")]
        public IActionResult BuyStockFromWatchlist([FromBody] BuyFromWatchlistRequest request)
        {
            try
            {
                // Validate request data
                if (request.UserId <= 0 || request.StockId <= 0 || request.Quantity <= 0)
                {
                    return BadRequest("Invalid request data. UserId, StockId, and Quantity are required.");
                }

                // Find the stock by ID
                var stock = _stockRepository.GetStock(request.StockId);
                if (stock == null)
                {
                    return BadRequest("Stock not found.");
                }

                // Retrieve user's watchlist
                var watchlist = _watchlistRepository.GetUserWatchlist(request.UserId);
                if (watchlist == null)
                {
                    return BadRequest("User watchlist not found.");
                }

                // Check if the stock exists in the user's watchlist
                if (!watchlist.Stocks.Any(s => s.StockID == request.StockId))
                {
                    return BadRequest("Stock not found in the user's watchlist.");
                }

                var portfolio = _portfolioRepository.GetPortfolio(request.UserId);
                if (portfolio == null)
                {
                    return BadRequest("User portfolio not found.");
                }

                // Call AddUserStockToPortfolio with both parameters
                _portfolioRepository.AddUserStockToPortfolio(portfolio.PortfolioID, request.UserId, request.StockId, request.Quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sellFromPortfolio")]
        public IActionResult SellStockFromPortfolio([FromBody] SellFromPortfolioRequest request)
        {
            try
            {
                // Validate request data
                if (request.UserId <= 0 || request.StockId <= 0 || request.Quantity <= 0)
                {
                    return BadRequest("Invalid request data. UserId, StockId, and Quantity are required.");
                }

                // Retrieve the specific UserStock item
                var userStock = _portfolioRepository.GetUserStock(request.UserId, request.StockId);
                if (userStock == null)
                {
                    return BadRequest("Stock not found in the user's portfolio.");
                }

                // Check if the user has enough quantity to sell
                if (userStock.Quantity < request.Quantity)
                {
                    return BadRequest("Insufficient quantity to sell.");
                }

                // Calculate total sell price
                decimal totalSellPrice = request.Quantity * userStock.Stock.CurrentPrice;

                // Update the UserStock quantity (subtract sold quantity)
                userStock.Quantity -= request.Quantity;

                // Update the UserStock in the repository (assuming UpdateUserStock exists)
                _portfolioRepository.UpdateUserStock(userStock);

                return Ok(totalSellPrice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class BuyFromWatchlistRequest
    {
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }

    public class SellFromPortfolioRequest
    {
        public int UserId { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}
