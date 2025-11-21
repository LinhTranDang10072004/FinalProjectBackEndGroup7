namespace HotelManagementApi.DTOs.Reposts
{
    public class YearlyRevenueDto
    {
        public int Year { get; set; }
        public decimal RoomRevenue { get; set; }
        public int TotalBookings { get; set; }
        public int TotalRoomNights { get; set; }
        public decimal AvgMonthlyRevenue => RoomRevenue / 12;
    }
}
