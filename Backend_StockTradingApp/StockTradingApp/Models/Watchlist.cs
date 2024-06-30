using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockTradingApp.Models
{
    public class Watchlist
    {
        [Key]
        public int WatchlistID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; } // Foreign key to identify the user who owns this watchlist

        public ICollection<Stock> Stocks { get; set; }
    }
}
