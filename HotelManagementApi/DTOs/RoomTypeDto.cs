namespace HotelManagementApi.DTOs
{
    public class RoomTypeDto
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; } = null!;
        public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        public string BedType { get; set; } = null!;
        public decimal AreaSqm { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}
