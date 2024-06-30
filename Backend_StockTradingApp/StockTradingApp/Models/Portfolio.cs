using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockTradingApp.Models
{
    public class Portfolio
    {
        [Key]
        public int PortfolioID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; } // Foreign key to identify the user who owns this portfolio   

        public ICollection<UserStock> UserStocks { get; set; }
    }
}