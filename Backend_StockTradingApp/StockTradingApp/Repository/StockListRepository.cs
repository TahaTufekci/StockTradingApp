using StockTradingApp.Data;
using StockTradingApp.Interfaces;
using StockTradingApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace StockTradingApp.Repository
{
    public class StockListRepository : IStockRepository
    {
        private readonly DataContext _context;

        public StockListRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Stock> GetAllStocks()
        {
            return _context.Stocks.ToList();
        }

        public Stock GetStock(int stockId)
        {
            return _context.Stocks.FirstOrDefault(s => s.StockID == stockId);
        }

        public void AddStock(Stock stock)
        {
            _context.Stocks.Add(stock);
            _context.SaveChanges();
        }

        public void UpdateStock(Stock stock)
        {
            _context.Stocks.Update(stock);
            _context.SaveChanges();
        }

        public void DeleteStock(int stockId)
        {
            var stock = _context.Stocks.Find(stockId);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                _context.SaveChanges();
            }
        }
    }
}
