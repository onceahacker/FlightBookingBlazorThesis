using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FlightBookingBlazorThesis.Shared
{
    public class Flight
    {
        public int Number { get; set; }
        public string Destination { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }

    }
}
