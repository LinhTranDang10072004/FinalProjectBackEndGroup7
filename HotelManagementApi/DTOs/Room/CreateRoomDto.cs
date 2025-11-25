using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Room
{
    public class CreateRoomDto
    {
        [Required, MaxLength(10)] public string RoomNumber { get; set; } = null!;
        public int Floor { get; set; }
        public int RoomTypeID { get; set; }
    }
}
