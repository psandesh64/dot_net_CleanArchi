﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Interfaces;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Services;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IDishService, DishService>();

        }
    }
}
