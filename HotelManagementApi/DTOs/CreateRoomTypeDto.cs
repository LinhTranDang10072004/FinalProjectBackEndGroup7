using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs
{
    public class CreateRoomTypeDto
    {
        [Required, MaxLength(50)] public string TypeName { get; set; } = null!;
        [Range(0.01, 100000000)] public decimal BasePrice { get; set; }
        public int Capacity { get; set; }
        [Required] public string BedType { get; set; } = null!;
        public decimal AreaSqm { get; set; }
        public string? Description { get; set; }
    }
}
