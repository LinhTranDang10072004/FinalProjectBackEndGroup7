namespace HotelManagementApi.DTOs.LoginUser
{
    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime Expires { get; set; }
        public UserInfo User { get; set; } = null!;
    }
}