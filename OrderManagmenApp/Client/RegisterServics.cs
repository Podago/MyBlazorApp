﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace OrderManagmen.Client
{
    public static class RegisterServics
    {
        public static void ConfigureServices(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
        }
    }
}
