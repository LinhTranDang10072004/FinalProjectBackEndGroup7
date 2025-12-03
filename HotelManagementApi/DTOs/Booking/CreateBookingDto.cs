using System.ComponentModel.DataAnnotations;

namespace HotelManagementApi.DTOs.Booking
{
    public class CreateBookingDto
    {
        [Required]
        public int UserID { get; set; }

        [Required]
        [Range(1, 20)]
        public int AdultCount { get; set; }

        [Range(0, 10)]
        public int ChildCount { get; set; } = 0;

        [Required]
        [Range(0.01, 100000000)]
        public decimal TotalAmount { get; set; }

        [Range(0, 100000000)]
        public decimal DepositAmount { get; set; } = 0;

        [MaxLength(1000)]
        public string? Notes { get; set; }

        // Booking Details - Một booking có thể có nhiều phòng
        [Required]
        [MinLength(1)]
        public List<CreateBookingDetailDto> BookingDetails { get; set; } = new();
    }

    public class CreateBookingDetailDto
    {
        [Required]
        public int RoomID { get; set; }

        [Required]
        public int RoomTypeID { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(0.01, 100000000)]
        public decimal PricePerNight { get; set; }
    }
}



