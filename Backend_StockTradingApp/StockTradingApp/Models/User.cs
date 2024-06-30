using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StockTradingApp.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Portfolio Portfolio { get; set; }
        public Watchlist Watchlist { get; set; } // Add Watchlist navigation property
    }
}