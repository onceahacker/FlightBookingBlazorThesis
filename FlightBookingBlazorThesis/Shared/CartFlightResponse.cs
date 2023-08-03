using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBookingBlazorThesis.Shared
{
    public class CartFlightResponse
    {
        public int FlightId { get; set; }
        public string Destination { get; set; } = string.Empty;
        public int FlightTypeId { get; set; }
        public string FlightType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}