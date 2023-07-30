﻿namespace FlightBookingBlazorThesis.Client.Services.FlightService
{
    public interface IFlightService
    {
        List<Flight> Flights { get; set; }
        Task GetFlights();
    }
}