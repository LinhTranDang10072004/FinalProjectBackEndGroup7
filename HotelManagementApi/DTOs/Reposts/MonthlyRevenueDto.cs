namespace HotelManagementApi.DTOs.Reposts
{
    public class MonthlyRevenueDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM yyyy");
        public decimal RoomRevenue { get; set; }
        public int TotalBookings { get; set; }
        public int TotalRoomNights { get; set; }
        public decimal AvgDailyRevenue { get; set; }
        public decimal OccupancyRate { get; set; }
        public decimal ADR { get; set; }
        public decimal RevPAR { get; set; }
    }
}
