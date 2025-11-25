using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email hoặc số điện thoại là bắt buộc")]
        public string EmailOrPhone { get; set; } = null!;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        public string Password { get; set; } = null!;
    }

    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public UserInfoDto? User { get; set; }
        public string? Message { get; set; }
    }

    public class UserInfoDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string UserRole { get; set; } = null!;
    }
}