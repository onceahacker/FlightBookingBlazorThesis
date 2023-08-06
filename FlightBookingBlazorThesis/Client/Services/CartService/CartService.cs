using Blazored.LocalStorage;

namespace FlightBookingBlazorThesis.Client.Services.NewFolder
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        private readonly IAuthService _authService;


        public CartService(ILocalStorageService localStorage, HttpClient http,
            IAuthService authService)
        {
            _localStorage = localStorage;
            _http = http;
            _authService = authService;

        }

        public event Action OnChange;

        public async Task AddToCart(CartItem cartItem)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.PostAsJsonAsync("api/cart/add", cartItem);
            }
            else
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
            }
            await GetCartItemsCount();
        }

        public async Task GetCartItemsCount()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var result = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
                var count = result.Data;

                await _localStorage.SetItemAsync<int>("cartItemsCount", count);
            }
            else
            {
                var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                await _localStorage.SetItemAsync<int>("cartItemsCount", cart != null ? cart.Count : 0);
            }

            OnChange.Invoke();
        }

        public async Task<List<CartFlightResponse>> GetCartFlights()
        {
            if (await _authService.IsUserAuthenticated())
            {
                var response = await _http.GetFromJsonAsync<ServiceResponse<List<CartFlightResponse>>>("api/cart");
                return response.Data;
            }
            else
            {
                var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
                if (cartItems == null)
                    return new List<CartFlightResponse>();
                var response = await _http.PostAsJsonAsync("api/cart/flights", cartItems);
                var cartFlights =
                    await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartFlightResponse>>>();
                return cartFlights.Data;
            }

        }

        public async Task RemoveFlightFromCart(int flightId, int flightTypeId)
        {
            if (await _authService.IsUserAuthenticated())
            {
                await _http.DeleteAsync($"api/cart/{flightId}/{flightTypeId}");
            }
            else
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
                }
            }
        }

        public async Task StoreCartItems(bool emptyLocalCart = false)
        {
            var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (localCart == null)
            {
                return;
            }

            await _http.PostAsJsonAsync("api/cart", localCart);

            if (emptyLocalCart)
            {
                await _localStorage.RemoveItemAsync("cart");

            }
        }

        public async Task UpdateQuantity(CartFlightResponse flight)
        {
            if (await _authService.IsUserAuthenticated())

            {
                var request = new CartItem
                {
                    FlightId = flight.FlightId,
                    Quantity = flight.Quantity,
                    FlightTypeId = flight.FlightTypeId
                };
                await _http.PutAsJsonAsync("api/cart/update-quantity", request);
            }

            else
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
}
    
