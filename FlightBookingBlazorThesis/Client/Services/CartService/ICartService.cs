﻿namespace FlightBookingBlazorThesis.Client.Services.NewFolder
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(CartItem cartItem);
        Task<List<CartFlightResponse>> GetCartFlights();
        Task RemoveFlightFromCart(int flightId, int flightTypeId);
        Task UpdateQuantity(CartFlightResponse flight);
        Task StoreCartItems(bool emptyLocalCart);
        Task GetCartItemsCount();
    }
}