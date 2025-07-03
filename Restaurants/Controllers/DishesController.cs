using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Interfaces;

namespace Restaurants.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DishesController(IDishService dishService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var dishes = await dishService.GetAllAsync();
        return Ok(dishes);
    }
}
