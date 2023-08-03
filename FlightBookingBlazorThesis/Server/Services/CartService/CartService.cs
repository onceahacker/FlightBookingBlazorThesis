namespace FlightBookingBlazorThesis.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CartFlightResponse>>> GetCartFlights(List<CartItem> cartItems)
        {
            var result = new ServiceResponse<List<CartFlightResponse>>
            {
                Data = new List<CartFlightResponse>()
            };

            foreach (var item in cartItems)
            {
                var flight = await _context.Flights
                    .Where(f => f.Number == item.FlightId)
                    .FirstOrDefaultAsync();

                if (flight == null)
                {
                    continue;
                }

                var flightVariant = await _context.FlightVariants
                    .Where(v => v.FlightId == item.FlightId
                        && v.FlightTypeId == item.FlightTypeId)
                    .Include(v => v.FlightType)
                    .FirstOrDefaultAsync();

                if (flightVariant == null)
                {
                    continue;
                }

                var cartFlight = new CartFlightResponse
                {
                    FlightId = flight.Number,
                    Destination = flight.Destination,
                    ImageUrl = flight.ImageUrl,
                    Price = flightVariant.Price,
                    FlightType = flightVariant.FlightType.Name,
                    FlightTypeId = flightVariant.FlightTypeId,
                    Quantity = item.Quantity
                };

                result.Data.Add(cartFlight);
            }

            return result;
        }
    }
}