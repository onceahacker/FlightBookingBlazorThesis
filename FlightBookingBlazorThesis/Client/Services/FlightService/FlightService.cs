using System.Net.Http.Json;
using System.Reflection.Metadata;

namespace FlightBookingBlazorThesis.Client.Services.FlightService
{
    public class FlightService : IFlightService
    {
        private readonly HttpClient _http;
        public FlightService(HttpClient http)
        {
            _http = http;
        }
        public List<Flight> Flights { get ; set; } = new List<Flight>();

        public async Task GetFlights()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Flight>>>("api/flight");
            if (result != null && result.Data != null)
                Flights = result.Data;
        }
    }
}
