using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Rating MessageType for Rating Model
 */
namespace BookStore.Services.MessageTypes
{
    public class Rating : MessageType
    {
        public bool Like { get; set; }
    }
}
