using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookingDetail
{
    [Key] public int BookingDetailID { get; set; }

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal PricePerNight { get; set; }

    // Computed
    [NotMapped] public int Nights => (CheckOutDate.Date - CheckInDate.Date).Days;
    [NotMapped] public decimal TotalCharge => PricePerNight * Nights;

    public int BookingID { get; set; }
    public int RoomID { get; set; }
    public int RoomTypeID { get; set; }

    public Booking Booking { get; set; } = null!;
    public Room Room { get; set; } = null!;
    public RoomType RoomType { get; set; } = null!;
}