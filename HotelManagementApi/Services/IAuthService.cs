using HotelManagementApi.DTOs;

namespace HotelManagementApi.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    }
}

