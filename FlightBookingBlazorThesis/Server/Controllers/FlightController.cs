using FlightBookingBlazorThesis.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBlazorThesis.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private static List<Flight> Flights = new List<Flight> {
        new Flight {
            Number = 1,
            Destination = "Dubai",
            Details = "Departuring from Budapest at 7:55 Arriving to Dubai at 13:30 Airlines: Fly emirates Duration: 5 hours and 35 mins",
            ImageUrl =  "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d0/Emirates_logo.svg/800px-Emirates_logo.svg.png",
            Price = 64999
        },

        new Flight {
            Number = 2,
            Destination = "Vienna",
            Details = "Departuring from Budapest at 8:20 Arriving to Dubai at 10:00 Airlines: Wizz Air Duration: 1 hours and 40 mins",
            ImageUrl =  "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
            Price = 8599
           },

           new Flight {
            Number = 3,
            Destination = "Copenhagen",
            Details = "Departuring from Budapest at 13:40 Arriving to Dubai at 15:50 Airlines: Wizz air Duration: 2 hours and 10 mins",
            ImageUrl =  "https://wizzair.com/static/images/default-source/press-office/logos/logos-without-url/wizz_logo_1_cl_baa8bb65.jpg",
            Price = 14999

            }
         };


        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetFlights() {
            return Ok(Flights);
        }
    }
}
