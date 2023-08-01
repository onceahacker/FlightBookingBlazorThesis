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

        public event Action FlightsChanged;

        public async Task<ServiceResponse<Flight>> GetFlight(int flightNumber)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Flight>>($"api/flight/{flightNumber}");
            return result;
        }

        public async Task GetFlights(string? categoryUrl = null)
        {
            var result = categoryUrl == null ?
                await _http.GetFromJsonAsync<ServiceResponse<List<Flight>>>("api/flight") :
                await _http.GetFromJsonAsync<ServiceResponse<List<Flight>>>($"api/flight/category/{categoryUrl}");
            if (result != null && result.Data != null)
                Flights = result.Data;

            FlightsChanged.Invoke();
        }

    }
}
