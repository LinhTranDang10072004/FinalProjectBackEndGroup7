using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagementApi.Data;
using HotelManagementApi.DTOs.Auth;
using HotelManagementApi.Services;
using BCrypt.Net;
using HotelManagementApi.DTOs;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(HotelDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            // 1. Tìm user theo email (không phân biệt hoa thường)
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.Trim().ToLower());

            // 2. Không tìm thấy → báo lỗi
            if (user == null)
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });

            // 3. Kiểm tra password bằng BCrypt
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isValidPassword)
                return Unauthorized(new { message = "Email hoặc mật khẩu không đúng" });

            // 4. Tạo JWT
            var token = _jwtService.GenerateToken(user);

            // 5. Trả về response đẹp
            var response = new LoginResponse
            {
                Token = token,
                Expires = DateTime.Now.AddHours(24),
                User = new UserInfo
                {
                    UserId = user.UserID,
                    Email = user.Email,
                    FullName = $"{user.FirstName} {user.LastName}".Trim(),
                    Role = user.UserRole
                }
            };

            return Ok(response);
        }
    }
}