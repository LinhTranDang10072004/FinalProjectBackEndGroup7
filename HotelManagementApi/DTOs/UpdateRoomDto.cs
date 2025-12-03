using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs
{
    public class UpdateRoomDto
    {
        [MaxLength(10)]
        public string? RoomNumber { get; set; }

        [Range(1, 100)]
        public int? Floor { get; set; }

        public bool? IsActive { get; set; }
    }
}
