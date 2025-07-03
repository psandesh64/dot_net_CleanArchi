using Microsoft.EntityFrameworkCore;
using Restaurants.Application.DTOs;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Services;

internal class DishService(RestaurantsDbContext dbContext) : IDishService
{
    public async Task<int> CreateAsync(CreateDishDto dto)
    {
        var dish = new Dish
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            RestaurantId = 1 // hardcoded for now
        };

        dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();

        return dish.Id;
    }
    public async Task<IEnumerable<DishDto>> GetAllAsync(decimal? priceUpto = null)
    {
        var query = dbContext.Dishes.AsQueryable();

        if (priceUpto is not null)
            query = query.Where(d => d.Price <= priceUpto);

        return await query
            .Select(d => new DishDto
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                Price = d.Price
            })
            .ToListAsync();
    }

    public async Task<DishDto?> GetByIdAsync(int id)
    {
        var dish = await dbContext.Dishes.FindAsync(id);
        if (dish is null) return null;

        return new DishDto
        {
            Id = dish.Id,
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price
        };
    }

    public async Task UpdateAsync(int id, DishDto dto)
    {
        var dish = await dbContext.Dishes.FindAsync(id);
        if (dish is null) throw new Exception("Dish not found.");

        dish.Name = dto.Name;
        dish.Description = dto.Description;
        dish.Price = dto.Price;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var dish = await dbContext.Dishes.FindAsync(id);
        if (dish is null) throw new Exception("Dish not found.");

        dbContext.Dishes.Remove(dish);
        await dbContext.SaveChangesAsync();
    }

}
