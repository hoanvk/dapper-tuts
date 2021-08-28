using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperTuts.Models
{
    public class Store
    {
        public int id { get; set; }
        public string storeName {get; set; }
        public string phone {get; set; }
        public string email {get; set; }
        public string street {get; set; }
        public string city {get; set; }
        public string state {get; set; }
        public string zipCode { get; set; }
        public ICollection<Stock> stocks { get; set; }
        public static Repositories.Store query()
        {
            return new Repositories.Store();
        }
    }
}
