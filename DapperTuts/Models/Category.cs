using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Models
{
    public class Category
    {
        public int id { get; set; }
        public string categoryName { get; set; }
        public ICollection<Product> products { get; set; }
        public static Repositories.Category query()
        {
            return new Repositories.Category();
        }
    }
}
