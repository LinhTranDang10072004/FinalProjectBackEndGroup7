using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Room
{
    [Key]
    public int RoomID { get; set; }

    [Required, MaxLength(10)] public string RoomNumber { get; set; } = null!;
    public int Floor { get; set; }
    [Required] public string Status { get; set; } = "Available"; 

    public bool IsActive { get; set; } = true;
    public int RoomTypeID { get; set; }
    public RoomType RoomType { get; set; } = null!;
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
    public ICollection<RoomStatusLog> StatusLogs { get; set; } = new List<RoomStatusLog>();
}