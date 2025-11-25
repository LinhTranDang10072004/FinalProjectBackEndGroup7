namespace HotelManagementApi.DTOs.Room
{
    public class RoomDto
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int Floor { get; set; }
        public string Status { get; set; } = null!;
        public bool IsActive { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; } = null!;
        public decimal BasePrice { get; set; }
    }
}
