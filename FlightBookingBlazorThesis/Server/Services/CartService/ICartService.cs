namespace FlightBookingBlazorThesis.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartFlightResponse>>> GetCartFlights(List<CartItem> cartItems);
    }
}