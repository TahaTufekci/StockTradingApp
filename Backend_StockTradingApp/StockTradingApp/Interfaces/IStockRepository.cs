using StockTradingApp.Models;
using System.Collections.Generic;

namespace StockTradingApp.Interfaces
{
    public interface IStockRepository
    {
        IEnumerable<Stock> GetAllStocks();
        Stock GetStock(int stockId);
    }
}