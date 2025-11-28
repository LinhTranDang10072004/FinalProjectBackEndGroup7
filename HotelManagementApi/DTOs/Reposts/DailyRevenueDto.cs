namespace HotelManagementApi.DTOs.Reposts
{
    public class DailyRevenueDto
    {
        public DateTime Date { get; set; }               // Ngày check-out
        public decimal RoomRevenue { get; set; }         // Doanh thu phòng
        public int CheckOutCount { get; set; }           // Số lượt check-out trong ngày
        public int RoomNights { get; set; }              // Tổng số đêm phòng bán ra
        public decimal OccupancyRate { get; set; }
        public decimal ADR { get; set; }
        public decimal RevPAR { get; set; }
    }
}
