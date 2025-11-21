using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Room
{
    public class UpdateRoomDto
    {
        [MaxLength(10)] public string? RoomNumber { get; set; }
        public int? Floor { get; set; }
        public bool? IsActive { get; set; }
    }
}
