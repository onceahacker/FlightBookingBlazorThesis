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
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Flight>>>> GetFlights() 
        {
            var result = await _flightService.GetFlightsAsync();
            return Ok(result);
        }

        [HttpGet("{flightNumber}")]
        public async Task<ActionResult<ServiceResponse<Flight>>> GetFlights(int flightNumber)
        {
            var result = await _flightService.GetFlightAsync(flightNumber);
            return Ok(result);
        }

        [HttpGet("category/{categoryUrl}")]
        public async Task<ActionResult<ServiceResponse<List<Flight>>>> GetFlightsByCategory(string categoryUrl)
        {
            var result = await _flightService.GetFlightsByCategory(categoryUrl);
            return Ok(result);
        }
        [HttpGet("search/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<Flight>>>> SearchFlights(string searchText)
        {
            var result = await _flightService.SearchFlights(searchText);
            return Ok(result);
        }
        [HttpGet("SearchSuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetFlightSearchSuggestions(string searchText)
        {
            var result = await _flightService.SearchFlights(searchText);
            return Ok(result);
        }
    }
}
