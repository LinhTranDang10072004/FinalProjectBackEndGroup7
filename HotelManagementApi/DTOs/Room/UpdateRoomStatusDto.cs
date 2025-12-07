using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Room
{
    /// <summary>
    /// DTO để cập nhật trạng thái phòng
    /// </summary>
    public class UpdateRoomStatusDto
    {
        /// <summary>
        /// Trạng thái mới của phòng. Các giá trị hợp lệ: 'Available', 'Booked', 'Occupied', 'Maintenance'
        /// </summary>
        [Required(ErrorMessage = "Status là bắt buộc")]
        [RegularExpression("^(Available|Booked|Occupied|Maintenance)$", ErrorMessage = "Status phải là một trong các giá trị: Available, Booked, Occupied, Maintenance")]
        public string Status { get; set; } = null!;
    }
}


