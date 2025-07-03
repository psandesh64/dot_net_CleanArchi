using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.DTOs;
using Restaurants.Application.Interfaces;

namespace Restaurants.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DishesController(IDishService dishService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDishDto dto)
    {
        var dishId = await dishService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = dishId }, new { id = dishId });
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] decimal? priceUpto = null)
    {
        var dishes = await dishService.GetAllAsync(priceUpto);
        return Ok(dishes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var dish = await dishService.GetByIdAsync(id);
        if (dish is null) return NotFound();
        return Ok(dish);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DishDto dto)
    {
        try
        {
            await dishService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await dishService.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}

