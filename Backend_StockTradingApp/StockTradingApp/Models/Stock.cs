using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockTradingApp.Models
{
    public class Stock
    {
        [Key]
        public int StockID { get; set; }
        public string StockName { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}