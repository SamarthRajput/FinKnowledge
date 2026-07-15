using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

// DTOS are used to give particular response back to the frontend
// for example to the user we just need to return email or his/her password are login so we will use dto there
namespace backend.Dtos.Stock
{

    public class StockDto
    {
        // Id of the stock that we save in our database
        public int Id { get; set; }
        // Symbol of the stock
        public string Symbol { get; set; } = string.Empty;
        // Company name of the stock
        public string CompanyName { get; set; } = string.Empty;
        public decimal Purchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        
    }
}