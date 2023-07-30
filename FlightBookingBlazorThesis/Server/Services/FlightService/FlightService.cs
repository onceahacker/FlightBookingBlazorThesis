namespace FlightBookingBlazorThesis.Server.Services.FlightService
{
    public class FlightService : IFlightService
    {
        private readonly DataContext _context;
        public FlightService(DataContext context)
        {
            _context = context;
        }

        public DataContext Context { get; }

        public async Task<ServiceResponse<Flight>> GetFlightAsync(int flightId)
        {
            var response = new ServiceResponse<Flight>();
            var flight = await _context.Flights.FindAsync(flightId);
            if (flight == null)
            {
                response.Success = false;
                response.Message = "Sorry, but this flight is fully booked or It doesnt exist.";
            }
            else
            {
                response.Data = flight;
            }

            return response;
        }

        public async Task<ServiceResponse<List<Flight>>> GetFlightsAsync()
        {
            var response = new ServiceResponse<List<Flight>>
            {
                Data = await _context.Flights.ToListAsync()
            };
            return response;
        }
    }
}
