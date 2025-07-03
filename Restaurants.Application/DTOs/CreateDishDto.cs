namespace Restaurants.Application.DTOs;

public class CreateDishDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}
