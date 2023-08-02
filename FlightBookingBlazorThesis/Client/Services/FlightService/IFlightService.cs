namespace FlightBookingBlazorThesis.Client.Services.FlightService
{
    public interface IFlightService
    {
        event Action FlightsChanged;
        List<Flight> Flights { get; set; }
        string Message { get; set; }
        Task GetFlights(string? categoryUrl = null);
        Task<ServiceResponse<Flight>> GetFlight(int flightNumber);
        Task SearchFlights(string searchText);
        Task<List<string>> GetFlightSearchSuggestions(string searchText);



    }
}
