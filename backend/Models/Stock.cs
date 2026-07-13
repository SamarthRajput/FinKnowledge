using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Stock
    {
        // Id of the stock that we save in our database
        public int Id { get; set; }
        // Symbol of the stock
        public string Symbol { get; set; } = string.Empty;
        // Company name of the stock
        public string CompanyName { get; set; } = string.Empty;
        // 18 digit and can have only 2 decimal places
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Purchase { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }

        // One to many relationship, we are using comments
        // List is a data structure that allow us to have many of something, like array in js 
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}