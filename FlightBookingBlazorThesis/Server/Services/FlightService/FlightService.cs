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

        public async Task<ServiceResponse<List<string>>> GetFlightsSearchSuggestions(string searchText)
        {
            var flights = await FindFlightsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var flight in flights)
            {
                if(flight.Destination.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(flight.Destination);
                }
               
                if(flight.Details != null)
                {
                    var punctuation = flight.Details.Where(char.IsPunctuation).Distinct().ToArray();
                    var words = flight.Details.Split().Select(w => w.Trim(punctuation));
                    foreach (var word in words)
                    {
                        if(word.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);

                        }
                    }
                }
            }
            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<List<Flight>>> SearchFlights(string searchText)
        {
            var response = new ServiceResponse<List<Flight>>
            {
                Data = await FindFlightsBySearchText(searchText)
            };
            return response;
        }

        private async Task<List<Flight>> FindFlightsBySearchText(string searchText)
        {
            return await _context.Flights
                                .Where(f => f.Destination.ToLower().Contains(searchText.ToLower())
                                || f.Destination.ToLower().Contains(searchText.ToLower()))
                                .Include(f => f.Variants)
                                .ToListAsync();
        }
    }
}
