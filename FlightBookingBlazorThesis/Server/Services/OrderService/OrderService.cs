using System.Security.Claims;


namespace FlightBookingBlazorThesis.Server.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly DataContext _context;
        private readonly ICartService _cartService;
        private readonly IAuthService _authService;

        public OrderService(DataContext context,
            ICartService cartService,
            IAuthService authService)
        {
            _context = context;
            _cartService = cartService;
            _authService = authService;
        }

        public async Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId)
        {
            var response = new ServiceResponse<OrderDetailsResponse>();
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Flight)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.FlightType)
                .Where(o => o.UserId == _authService.GetUserId() && o.Id == orderId)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                response.Success = false;
                response.Message = "Order not found.";
                return response;
            }

            var orderDetailsResponse = new OrderDetailsResponse
            {
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Flights = new List<OrderDetailsFlightResponse>()
            };

            order.OrderItems.ForEach(item =>
            orderDetailsResponse.Flights.Add(new OrderDetailsFlightResponse
            {
                FlightId = item.FlightId,
                ImageUrl = item.Flight.ImageUrl,
                FlightType = item.FlightType.Name,
                Quantity = item.Quantity,
                Destination = item.Flight.Destination,
                TotalPrice = item.TotalPrice
            }));

            response.Data = orderDetailsResponse;

            return response;
        }

        public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
        {
            var response = new ServiceResponse<List<OrderOverviewResponse>>();
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Flight)
                .Where(o => o.UserId == _authService.GetUserId())
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            var orderResponse = new List<OrderOverviewResponse>();
            orders.ForEach(o => orderResponse.Add(new OrderOverviewResponse
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice,
                Flight = o.OrderItems.Count > 1 ?
                    $"{o.OrderItems.First().Flight.Destination} and" +
                    $" {o.OrderItems.Count - 1} more..." :
                    o.OrderItems.First().Flight.Destination,
                FlightImageUrl = o.OrderItems.First().Flight.ImageUrl
            }));

            response.Data = orderResponse;

            return response;
        }

        public async Task<ServiceResponse<bool>> PlaceOrder()
        {
            var flights = (await _cartService.GetDbCartFlights()).Data;
            decimal totalPrice = 0;
            flights.ForEach(flight => totalPrice += flight.Price * flight.Quantity);

            var orderItems = new List<OrderItem>();
            flights.ForEach(flight => orderItems.Add(new OrderItem
            {
                FlightId = flight.FlightId,
                FlightTypeId = flight.FlightTypeId,
                Quantity = flight.Quantity,
                TotalPrice = flight.Price * flight.Quantity
            }));

            var order = new Order
            {
                UserId = _authService.GetUserId(),
                OrderDate = DateTime.Now,
                TotalPrice = totalPrice,
                OrderItems = orderItems
            };

            _context.Orders.Add(order);

            _context.CartItems.RemoveRange(_context.CartItems
                .Where(ci => ci.UserId == _authService.GetUserId()));

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true };
        }
    }
}
