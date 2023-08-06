global using FlightBookingBlazorThesis.Shared;
global using System.Net.Http.Json;
global using FlightBookingBlazorThesis.Client.Services.FlightService;
global using FlightBookingBlazorThesis.Client.Services.CategoryService;
global using FlightBookingBlazorThesis.Client.Services.AuthService;
global using Microsoft.AspNetCore.Components.Authorization;
global using FlightBookingBlazorThesis.Client.Services.NewFolder;
global using FlightBookingBlazorThesis.Client.Services.OrderService;
using FlightBookingBlazorThesis.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


await builder.Build().RunAsync();
