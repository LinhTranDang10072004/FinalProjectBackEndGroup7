namespace HotelManagementApi.DTOs.Booking
{
    public class BookingDto
    {
        public int BookingID { get; set; }
        public string BookingCode { get; set; } = null!;
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public int AdultCount { get; set; }
        public int ChildCount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public string BookingStatus { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CancelReason { get; set; }
        public DateTime? CancelDate { get; set; }
        public List<BookingDetailDto> BookingDetails { get; set; } = new();
    }

    public class BookingDetailDto
    {
        public int BookingDetailID { get; set; }
        public int RoomID { get; set; }
        public string RoomNumber { get; set; } = null!;
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Nights { get; set; }
        public decimal PricePerNight { get; set; }
        public decimal TotalCharge { get; set; }
    }
}






