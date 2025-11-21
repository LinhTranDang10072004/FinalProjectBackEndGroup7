using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class RoomType
{
    [Key]
    public int RoomTypeID { get; set; }
    [Required, MaxLength(50)] public string TypeName { get; set; } = null!;
    [Column(TypeName = "decimal(18,2)")] public decimal BasePrice { get; set; }
    public int Capacity { get; set; }
    [Required, MaxLength(30)] public string BedType { get; set; } = null!;
    [Column(TypeName = "decimal(5,2)")] public decimal AreaSqm { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}