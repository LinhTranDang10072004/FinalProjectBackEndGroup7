using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs
{
    public class CreateRoomDto
    {
        [Required, MaxLength(10)]
        public string RoomNumber { get; set; } = null!;

        [Required]
        [Range(1, 100)]
        public int Floor { get; set; }

        [Required]
        public int RoomTypeID { get; set; }
    }
}
