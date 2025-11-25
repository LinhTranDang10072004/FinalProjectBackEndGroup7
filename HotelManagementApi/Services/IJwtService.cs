using HotelManagementApi.Models;
using System.Security.Claims;

namespace HotelManagementApi.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
