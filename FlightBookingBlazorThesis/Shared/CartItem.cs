using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingBlazorThesis.Shared
{
    public class CartItem
    {
        public int FlightId { get; set; }
        public int FlightTypeId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}