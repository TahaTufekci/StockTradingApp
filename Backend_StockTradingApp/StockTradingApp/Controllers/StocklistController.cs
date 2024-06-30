using Microsoft.AspNetCore.Mvc;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;

namespace StockTradingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocklistController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StocklistController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Stock>))]
        public IActionResult GetAllStocks()
        {
            var stocks = _stockRepository.GetAllStocks();
            return Ok(stocks);
        }
        [HttpGet("{stockId}")]
        [ProducesResponseType(200, Type = typeof(Stock))]
        public IActionResult GetStockDetails(int stockId)
        {
            var stockDetails = _stockRepository.GetStock(stockId);
            if (stockDetails == null)
            {
                return NotFound();
            }
            return Ok(stockDetails);
        }
    }
}
