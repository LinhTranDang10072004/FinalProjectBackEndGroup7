using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Room
{
    public class UpdateRoomDto
    {
        public string? RoomNumber { get; set; }
        public int? Floor { get; set; }
        public string? Status { get; set; }
        public int? RoomTypeID { get; set; }
        public bool? IsActive { get; set; }
    }
}
