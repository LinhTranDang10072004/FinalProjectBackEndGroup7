using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs
{
    /// <summary>
    /// DTO để cập nhật thông tin phòng
    /// </summary>
    public class UpdateRoomDto
    {
        /// <summary>
        /// Số phòng (tối đa 10 ký tự)
        /// </summary>
        [MaxLength(10)]
        public string? RoomNumber { get; set; }

        /// <summary>
        /// Tầng (từ 1 đến 100)
        /// </summary>
        [Range(1, 100)]
        public int? Floor { get; set; }

        /// <summary>
        /// Trạng thái hoạt động của phòng (true = đang hoạt động, false = không hoạt động)
        /// </summary>
        public bool? IsActive { get; set; }

        /// <summary>
        /// Trạng thái phòng. Các giá trị hợp lệ: 'Available', 'Booked', 'Occupied', 'Maintenance'
        /// </summary>
        [RegularExpression("^(Available|Booked|Occupied|Maintenance)$", ErrorMessage = "Status phải là một trong các giá trị: Available, Booked, Occupied, Maintenance")]
        public string? Status { get; set; }
    }
}
