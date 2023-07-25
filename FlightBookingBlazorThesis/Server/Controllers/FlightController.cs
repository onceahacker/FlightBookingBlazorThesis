using FlightBookingBlazorThesis.Server.Data;
using FlightBookingBlazorThesis.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBlazorThesis.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly DataContext _context;

        public FlightController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Flight>>> GetFlights() 
        {
            var flights = await _context.Flights.ToListAsync();
            return Ok(flights);
        }
    }
}
