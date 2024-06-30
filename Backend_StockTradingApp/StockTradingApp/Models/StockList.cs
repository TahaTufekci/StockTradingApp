namespace StockTradingApp.Models
{
    public class StockList
    {
        public int StockListID { get; set; }
        public ICollection<Stock> ListStock { get; set; }
    }
}