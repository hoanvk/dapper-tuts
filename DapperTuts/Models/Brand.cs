using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Models
{
    public class Brand
    {
        public int id { get; set; }
        public string brandName { get; set; }
        public ICollection<Product> products { get; set; }
    }
}
