using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingBlazorThesis.Shared
{
    public class OrderDetailsFlightResponse
    {
        public int FlightId { get; set; }
        public string Destination { get; set; }
        public string FlightType { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}