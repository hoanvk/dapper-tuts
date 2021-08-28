using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Models
{
    public class Product
    {
        public int id { get; set; }
        public string productName { get; set; }
        public int brandId { get; set; }
        public int categoryId { get; set; }
        public int modelYear { get; set; }
        public decimal listPrice { get; set; }
        public Brand brand { get; set; }
        public Category category { get; set; }
        
        public static Repositories.Product query()
        {
            return new Repositories.Product();
        }
    }
}
