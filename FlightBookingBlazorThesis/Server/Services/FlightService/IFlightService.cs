namespace FlightBookingBlazorThesis.Server.Services.FlightService
{
    public interface IFlightService
    {
        Task<ServiceResponse<List<Flight>>> GetFlightsAsync();
        Task<ServiceResponse<Flight>> GetFlightAsync(int flightId);
    }
}
