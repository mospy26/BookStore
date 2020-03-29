using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * Purchase MessageType for Purchase Model
 */
namespace BookStore.Services.MessageTypes
{
    public class Purchase : MessageType
    {
        public User User { get; set; }

        public Media Media { get; set; }
    }
}
