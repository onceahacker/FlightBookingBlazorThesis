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
        public string Message { get; set; } = "Loading Flights...";

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

        public async Task<List<string>> GetFlightSearchSuggestions(string searchText)
        {
            try
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/flight/SearchSuggestions/{searchText}");
                return result?.Data ?? new List<string>();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log the error, display an error message)
                // For simplicity, let's set the Message property to the exception message
                Message = $"Error: {ex.Message}";
                return new List<string>(); // Return an empty list or appropriate fallback value
            }
            finally
            {
                // Always call StateHasChanged after handling the exception
                FlightsChanged?.Invoke();
            }
        }

            public async Task SearchFlights(string searchText)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Flight>>>($"api/flight/search/{searchText}");
            Flights = result.Data;
            if(Flights.Count==0)
            {
                Message = "No Flights found.";
            }
            FlightsChanged.Invoke();
        }
    }
}
