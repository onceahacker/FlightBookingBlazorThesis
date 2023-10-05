using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FlightBookingBlazorThesis.Shared
{
    public class Flight
    {
        [Key]
        public int Number { get; set; }
        public string Destination { get; set; } = string.Empty;
        public string DepartureCity { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty; 
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }

        public Category? Category { get; set; }
        public int CategoryId { get; set; } 
        public List<FlightVariant> Variants { get; set; } = new List<FlightVariant>();
    }
}
