using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightBookingBlazorThesis.Shared
{
    public class FlightVariant
    {
        [JsonIgnore]
        public Flight Flight { get; set; }
        public int FlightId { get; set; }
        public FlightType FlightType { get; set; }
        public int FlightTypeId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }

    }
}
