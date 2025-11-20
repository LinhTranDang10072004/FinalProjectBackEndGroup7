using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        // 1. Ai cũng vào được (không cần token)
        [HttpGet("public")]
        public IActionResult Public() => Ok("Ai cũng thấy được!");

        // 2. CHỈ ai có token hợp lệ mới vào được
        [Authorize]
        [HttpGet("private")]
        public IActionResult Private()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new
            {
                message = "Bạn đã đăng nhập thành công!",
                userId,
                email,
                role,
                time = DateTime.Now
            });
        }

        // 3. CHỈ ADMIN mới vào được
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly() => Ok("Chỉ Admin mới vào được đây nè!");

        // 4. CHỈ Staff (Admin + Receptionist) mới vào được
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpGet("staff-only")]
        public IActionResult StaffOnly() => Ok("Admin và Receptionist vào được!");
    }
}