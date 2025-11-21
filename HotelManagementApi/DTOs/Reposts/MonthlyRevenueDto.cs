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
        public decimal AvgDailyRevenue => DateTime.DaysInMonth(Year, Month) > 0
            ? Math.Round(RoomRevenue / DateTime.DaysInMonth(Year, Month), 2)
            : 0;
    }
}
