using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Invoice
{
    [Key] public int InvoiceID { get; set; }
    [Required, MaxLength(15)] public string InvoiceCode { get; set; } = null!;
    public DateTime IssueDate { get; set; } = DateTime.Now;
    [Column(TypeName = "decimal(18,2)")] public decimal TotalRoomCharge { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal TotalServiceCharge { get; set; } = 0;
    [Column(TypeName = "decimal(18,2)")] public decimal TotalDiscount { get; set; } = 0;
    [Column(TypeName = "decimal(5,2)")] public decimal VATPercent { get; set; } = 10;
    [Column(TypeName = "decimal(18,2)")] public decimal PaidAmount { get; set; } = 0;
    public string PaymentStatus { get; set; } = "Complete";

    public int BookingID { get; set; }
    public Booking Booking { get; set; } = null!;
}