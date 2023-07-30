namespace FlightBookingBlazorThesis.Client.Services.FlightService
{
    public interface IFlightService
    {
        event Action FlightsChanged;
        List<Flight> Flights { get; set; }
        Task GetFlights(string? categoryUrl = null);
        Task<ServiceResponse<Flight>> GetFlight(int flightNumber);
    }
}
