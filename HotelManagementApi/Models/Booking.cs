using HotelManagementApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Booking
{
    [Key] public int BookingID { get; set; }
    [Required, MaxLength(12)] public string BookingCode { get; set; } = null!;
    public int AdultCount { get; set; }
    public int ChildCount { get; set; } = 0;
    [Column(TypeName = "decimal(18,2)")] public decimal TotalAmount { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal DepositAmount { get; set; } = 0;
    public string BookingStatus { get; set; } = "Confirmed";
    [MaxLength(1000)] public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [MaxLength(500)] public string? CancelReason { get; set; }
    public DateTime? CancelDate { get; set; }

    public int UserID { get; set; }
    public User User { get; set; } = null!;
    public Invoice? Invoice { get; set; }
    public ICollection<BookingDetail> BookingDetails { get; set; } = new List<BookingDetail>();
 
}