using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementApi.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required, MaxLength(50)] public string FirstName { get; set; } = null!;
        [Required, MaxLength(50)] public string LastName { get; set; } = null!;
        [Required, MaxLength(15)] public string Phone { get; set; } = null!;
        [Required, MaxLength(100)] public string Email { get; set; } = null!;
        [Required, MaxLength(255)] public string PasswordHash { get; set; } = null!;
        [MaxLength(20)] public string? IdCard { get; set; }
        [MaxLength(50)] public string Nationality { get; set; } = "VietNam";
        [Required, MaxLength(255)] public string Address { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        [MaxLength(20)] public string Gender { get; set; } = "Other";
        [MaxLength(500)] public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required, Column(TypeName = "nvarchar(20)")]
        [MaxLength(20)] public string UserRole { get; set; } = "Customer";

        // Navigation
        public ICollection<RoomStatusLog> RoomStatusLogs { get; set; } = new List<RoomStatusLog>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}