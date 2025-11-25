using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Room
{
    public class CreateRoomDto
    {
        public string RoomNumber { get; set; } = null!;
        public int Floor { get; set; }
        public int RoomTypeID { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
