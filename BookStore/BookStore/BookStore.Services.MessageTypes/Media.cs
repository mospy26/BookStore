using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.MessageTypes
{
    public class Media : MessageType
    {
        public String Title { get; set; }
        public String Director { get; set; }
        public String Genre { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}
