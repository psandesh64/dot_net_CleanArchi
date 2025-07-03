using Microsoft.EntityFrameworkCore;
using Restaurants.Application.DTOs;
using Restaurants.Application.Interfaces;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Services;

internal class DishService(RestaurantsDbContext dbContext) : IDishService
{
    public async Task<IEnumerable<DishDto>> GetAllAsync()
    {
        return await dbContext.Dishes
            .Select(d => new DishDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price
            })
            .ToListAsync();
    }
}
