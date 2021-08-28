using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Models
{
    public class Stock
    {
        public int id { get; set; }       
        public int storeId { get; set; }
        public int productId { get; set; }
        public int quantity { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public Product product { get; set; }
        public Store store { get; set; }

        public static Repositories.Stock query()
        {
            return new Repositories.Stock();
        }
    }
}
