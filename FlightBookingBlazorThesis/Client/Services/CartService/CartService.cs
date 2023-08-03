using Blazored.LocalStorage;

namespace FlightBookingBlazorThesis.Client.Services.NewFolder
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CartService (ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            var sameItem = cart.Find(x => x.FlightId == cartItem.FlightId &&
                x.FlightTypeId == cartItem.FlightTypeId);
            if (sameItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                cart = new List<CartItem>();
            }

            return cart;
        }

        public async Task<List<CartFlightResponse>> GetCartFlights()
        {
            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            var response = await _http.PostAsJsonAsync("api/cart/flights", cartItems);
            var cartFlights =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartFlightResponse>>>();
            return cartFlights.Data;
        }

        public async Task RemoveFlightFromCart(int flightId, int flightTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.FlightId == flightId
                && x.FlightTypeId == flightTypeId);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart", cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateQuantity(CartFlightResponse flight)
        {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
                return;
            }

            var cartItem = cart.Find(x => x.FlightId == flight.FlightId
                && x.FlightTypeId == flight.FlightTypeId);
            if (cartItem != null)
            {
                cartItem.Quantity = flight.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}