namespace HotelManagementApi.DTOs.Room
{
    /// <summary>
    /// DTO chi tiết đầy đủ thông tin của phòng
    /// </summary>
    public class RoomDetailDto
    {
        /// <summary>
        /// ID của phòng
        /// </summary>
        public int RoomID { get; set; }

        /// <summary>
        /// Số phòng (ví dụ: "101", "202")
        /// </summary>
        public string RoomNumber { get; set; } = null!;

        /// <summary>
        /// Tầng của phòng
        /// </summary>
        public int Floor { get; set; }

        /// <summary>
        /// Trạng thái phòng: Available, Booked, Occupied, Maintenance
        /// </summary>
        public string Status { get; set; } = null!;

        /// <summary>
        /// Phòng có đang hoạt động không
        /// </summary>
        public bool IsActive { get; set; }

        // Thông tin loại phòng
        /// <summary>
        /// ID loại phòng
        /// </summary>
        public int RoomTypeID { get; set; }

        /// <summary>
        /// Tên loại phòng (ví dụ: "Standard", "Deluxe", "Suite")
        /// </summary>
        public string RoomTypeName { get; set; } = null!;

        /// <summary>
        /// Giá cơ bản mỗi đêm (VND)
        /// </summary>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// Sức chứa (số người)
        /// </summary>
        public int Capacity { get; set; }

        /// <summary>
        /// Loại giường (ví dụ: "Single", "Double", "King Size")
        /// </summary>
        public string BedType { get; set; } = null!;

        /// <summary>
        /// Diện tích phòng (m²)
        /// </summary>
        public decimal AreaSqm { get; set; }

        /// <summary>
        /// Mô tả chi tiết về phòng
        /// </summary>
        public string? Description { get; set; }
    }
}

