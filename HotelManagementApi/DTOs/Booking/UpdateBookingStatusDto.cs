using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Booking
{
    public class UpdateBookingStatusDto
    {
        [Required]
        [RegularExpression("^(Confirmed|Check-in|Check-out|Cancelled|No-show)$", 
            ErrorMessage = "Status must be one of: Confirmed, Check-in, Check-out, Cancelled, No-show")]
        public string BookingStatus { get; set; } = null!;

        [MaxLength(500)]
        public string? CancelReason { get; set; }
    }
}



