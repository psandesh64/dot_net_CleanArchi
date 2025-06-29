using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = [
                new()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC is an AMerican food Company",
                    ContactEmail = "caont@yahoo.com",
                    HasDelivery = true,
                    Dishes =
                    [
                        new()
                        {
                            Name = "Nashville Hot Chicken",
                            Description = "Nash (10pcs)",
                            Price = 10.30M,
                        },
                        new()
                        {
                            Name = "Chicken Nuggets",
                            Description = "Nuggets (5pcs)",
                            Price = 5.30M,
                        },
                    ],
                    Address = new()
                    {
                        City = "London",
                        Street = "Cork St 5",
                        PostalCode = "WC2N 5DU",
                    }
                },

            ];
            return restaurants;
        }

    }
}
