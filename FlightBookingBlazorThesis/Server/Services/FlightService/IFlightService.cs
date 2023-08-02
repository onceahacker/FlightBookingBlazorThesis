namespace FlightBookingBlazorThesis.Server.Services.FlightService
{
    public interface IFlightService
    {
        Task<ServiceResponse<List<Flight>>> GetFlightsAsync();
        Task<ServiceResponse<Flight>> GetFlightAsync(int flightId);
        Task<ServiceResponse<List<Flight>>> GetFlightsByCategory(string categoryUrl);
        Task<ServiceResponse<List<Flight>>> SearchFlights(string searchText);
        Task<ServiceResponse<List<string>>> GetFlightsSearchSuggestions(string searchText);
    }
}
