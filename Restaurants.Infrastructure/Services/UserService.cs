using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurants.Application.DTOs;
using Restaurants.Application.Interfaces;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurants.Infrastructure.Services;

internal class UserService(
    RestaurantsDbContext dbContext,
    IConfiguration configuration,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    public async Task RegisterAsync(RegisterUserDto dto)
    {
        if (await dbContext.Users.AnyAsync(u => u.Username == dto.Username))
            throw new Exception("User already exists.");

        var user = new User
        {
            Username = dto.Username
        };

        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginUserDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);
        if (user is null)
            throw new Exception("Invalid username or password.");

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid username or password.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["JwtSettings:Issuer"],
            audience: configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
