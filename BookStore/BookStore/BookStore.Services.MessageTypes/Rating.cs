using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.MessageTypes
{
    public class Rating : MessageType
    {
        public bool Like { get; set; }

        public Media Media { get; set; }

        public User UserId { get; set; }
    }
}
