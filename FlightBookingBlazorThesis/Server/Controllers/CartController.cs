using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBlazorThesis.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("flights")]
        public async Task<ActionResult<ServiceResponse<List<CartFlightResponse>>>> GetCartFlights(List<CartItem> cartItems)
        {
            var result = await _cartService.GetCartFlights(cartItems);
            return Ok(result);
        }
    }
}