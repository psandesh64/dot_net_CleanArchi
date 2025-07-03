using Restaurants.Application.DTOs;

namespace Restaurants.Application.Interfaces;

public interface IDishService
{
    Task<IEnumerable<DishDto>> GetAllAsync();
}
