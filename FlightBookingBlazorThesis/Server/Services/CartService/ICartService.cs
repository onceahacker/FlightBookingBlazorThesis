namespace FlightBookingBlazorThesis.Server.Services.CartService
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartFlightResponse>>> GetCartFlights(List<CartItem> cartItems);
        Task<ServiceResponse<List<CartFlightResponse>>> StoreCartItems(List<CartItem> cartItems);
        Task<ServiceResponse<int>> GetCartItemsCount();
        Task<ServiceResponse<List<CartFlightResponse>>> GetDbCartFlights();
        Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
        Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);
        Task<ServiceResponse<bool>> RemoveItemFromCart(int flighttId, int flightTypeId);
    }
}