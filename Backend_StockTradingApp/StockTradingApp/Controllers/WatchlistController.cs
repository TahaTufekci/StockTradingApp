using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System;
using System.Linq;

namespace StockTradingApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class WatchlistController : ControllerBase
  {
    private readonly IWatchlistRepository _watchlistRepository;
    private readonly IStockRepository _stockRepository;

    public WatchlistController(IWatchlistRepository watchlistRepository, IStockRepository stockRepository)
    {
      _watchlistRepository = watchlistRepository;
      _stockRepository = stockRepository;
    }

    [HttpPost("add")]
    public IActionResult AddToWatchlist([FromQuery] int userId, [FromQuery] int stockId)
    {
      try
      {
        // Validate request data (assuming UserId and StockId are required)
        if (userId <= 0 || stockId <= 0)
        {
          return BadRequest("Invalid request data. UserId and StockId are required.");
        }

        // Check if a watchlist already exists for the user
        var watchlist = _watchlistRepository.GetUserWatchlist(userId);
        if (watchlist == null)
        {
          // Create a new watchlist if it doesn't exist
          watchlist = new Watchlist { UserID = userId };
          _watchlistRepository.AddWatchlist(watchlist); // Assuming AddWatchlist exists in repository
        }

        // Check if the stock already exists in the watchlist
        if (watchlist.Stocks?.Any(s => s.StockID == stockId) == true)
        {
          return BadRequest("Stock already exists in the watchlist.");
        }

        // Add the user stock to the portfolio (assuming AddUserStock exists)
       _watchlistRepository.AddStockToWatchlist(stockId, userId);

        // Update the watchlist in the repository
        _watchlistRepository.UpdateWatchlist(watchlist);
        return Ok();
       }
       catch (DbUpdateConcurrencyException ex)
       {
        return Conflict(ex.Message); // Handle concurrency exception
       }
       catch (Exception ex)
       {
        return BadRequest(ex.Message);
       }
    }

    [HttpGet]
    public IActionResult GetWatchlist(int userId)
    {
      try
      {
        // Validate user ID (optional)
        if (userId <= 0)
        {
          return BadRequest("Invalid user ID.");
        }

        // Retrieve the watchlist for the user
        var watchlist = _watchlistRepository.GetUserWatchlist(userId);
        if (watchlist == null)
        {
          return NotFound("Watchlist not found for the provided user.");
        }

        // Return the watchlist object (consider filtering or including related data)
        return Ok(watchlist);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("remove")]
    public IActionResult RemoveFromWatchlist([FromQuery] int userId, [FromQuery] int stockId)
    {
      try
      {
        // Validate request data (assuming UserId and StockId are required)
        if (userId <= 0 || stockId <= 0)
        {
          return BadRequest("Invalid request data. UserId and StockId are required.");
        }

        // Get the watchlist for the user
        var watchlist = _watchlistRepository.GetUserWatchlist(userId);
        if (watchlist == null)
        {
          return NotFound("Watchlist not found for the provided user.");
        }

        // Find the stock in the watchlist (assuming unique stock IDs)
        var stockToRemove = watchlist.Stocks?.FirstOrDefault(s => s.StockID == stockId);
        if (stockToRemove == null)
        {
          return NotFound("Stock not found in the watchlist.");
        }

        // Remove the stock from the watchlist's Stocks collection
        _watchlistRepository.RemoveStockFromWatchlist(userId, stockId);

        // Update the watchlist in the repository
        _watchlistRepository.UpdateWatchlist(watchlist);
        return Ok();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    public class WatchlistItemRequest
    {
      public int UserId { get; set; }
      public int StockId { get; set; }
    }
  }
}
