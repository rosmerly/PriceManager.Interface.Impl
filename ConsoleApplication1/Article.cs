using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Article
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public IList<Discount> Discounts { get; set; }
    }
}
