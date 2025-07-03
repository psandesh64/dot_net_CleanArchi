using Restaurants.Application.DTOs;

namespace Restaurants.Application.Interfaces;

public interface IUserService
{
    Task RegisterAsync(RegisterUserDto dto);
    Task<string> LoginAsync(LoginUserDto dto);
}
