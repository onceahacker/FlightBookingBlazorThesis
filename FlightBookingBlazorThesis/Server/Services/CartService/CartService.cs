using System.Security.Claims;

namespace FlightBookingBlazorThesis.Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public CartService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;

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


        public async Task<ServiceResponse<List<CartFlightResponse>>> StoreCartItems(List<CartItem> cartItems)
        {
            cartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());
            _context.CartItems.AddRange(cartItems);
            await _context.SaveChangesAsync();

            return await GetDbCartFlights();
        }

        public async Task<ServiceResponse<int>> GetCartItemsCount()
        {
            var count = (await _context.CartItems.Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync()).Count;
            return new ServiceResponse<int> { Data = count };
        }

        public async Task<ServiceResponse<List<CartFlightResponse>>> GetDbCartFlights()
        {
            return await GetCartFlights(await _context.CartItems
                .Where(ci => ci.UserId == _authService.GetUserId()).ToListAsync());
        }

        public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
        {
            cartItem.UserId = _authService.GetUserId();

            var sameItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.FlightId == cartItem.FlightId &&
                ci.FlightTypeId == cartItem.FlightTypeId && ci.UserId == cartItem.UserId);
            if (sameItem == null)
            {
                _context.CartItems.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
        

        public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
        {
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.FlightId == cartItem.FlightId &&
                ci.FlightTypeId == cartItem.FlightTypeId && ci.UserId == _authService.GetUserId());
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            dbCartItem.Quantity = cartItem.Quantity;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<bool>> RemoveItemFromCart(int flightId, int flightTypeId)
        {
            var dbCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.FlightId == flightId &&
                ci.FlightTypeId == flightTypeId && ci.UserId == _authService.GetUserId());
            if (dbCartItem == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Cart item does not exist."
                };
            }

            _context.CartItems.Remove(dbCartItem);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}