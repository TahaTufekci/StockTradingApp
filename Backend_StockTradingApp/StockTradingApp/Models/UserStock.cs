namespace StockTradingApp.Models
{
    public class UserStock
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int StockID { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Stock Stock { get; set; }
    }
}