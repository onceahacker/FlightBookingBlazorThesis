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
            var flight = await _context.Flights
                .Include(f => f.Variants).
                ThenInclude(v => v.FlightType).
                FirstOrDefaultAsync(f => f.Number == flightId);
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
            var flights = await _context.Flights.Include(f => f.Variants).ToListAsync();
            var response = new ServiceResponse<List<Flight>>
            {
                Data = await _context.Flights.ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Flight>>> GetFlightsByCategory(string categoryUrl)
        {
            var response = new ServiceResponse<List<Flight>>
            {
                Data = await _context.Flights
                    .Where(f => f.Category.Url.ToLower().Equals(categoryUrl.ToLower()))
                    .Include(f => f.Variants)
                    .ToListAsync()
            };
            return response;
        }

        public async Task<ServiceResponse<List<Flight>>> SearchFlights(string searchText)
        {
            var response = new ServiceResponse<List<Flight>>
            {
                Data = await _context.Flights
                    .Where(f => f.Destination.ToLower().Contains(searchText.ToLower())
                    || f.Destination.ToLower().Contains(searchText.ToLower()))
                    .Include(f => f.Variants)
                    .ToListAsync()
            };
            return response;
        }
    }
}
