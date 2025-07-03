using Restaurants.Application.DTOs;

namespace Restaurants.Application.Interfaces;

public interface IDishService
{
    Task<int> CreateAsync(CreateDishDto dto);
    Task<IEnumerable<DishDto>> GetAllAsync(decimal? priceUpto = null);
    Task<DishDto?> GetByIdAsync(int id);
    Task UpdateAsync(int id, DishDto dto);
    Task DeleteAsync(int id);
}
