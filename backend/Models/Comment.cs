using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    // User Can comment below the stock
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;



        // There are many to form 1 to many relationship, but we are doing using convention
        // Convention means entity framework 
        public int? MyProperty { get; set; }
        // // Navigation property, it help us to navigate to our models 
        public Stock? Stock { get; set; }
    }
}