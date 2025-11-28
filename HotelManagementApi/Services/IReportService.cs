using HotelManagementApi.DTOs.Reposts;

namespace HotelManagementApi.Services
{
    public interface IReportService
    {
        // 1. Doanh thu 1 ngày cụ thể (đêm * giá 1 đêm )
        Task<decimal> GetRevenueByDateAsync(DateTime date);

        // 2. Doanh thu từ ngày đến ngày (mới - dùng cho tổng từ đầu và range)
        Task<decimal> GetRevenueByDateRangeAsync(DateTime fromDate, DateTime toDate);

        // 3. Doanh thu 1 tháng nhập năm, tháng => select hết các ngày trong năm,tháng đó
        Task<List<DailyRevenueDto>> GetRevenueByMonthAsync(int year, int month);

        // 4. Doanh thu 12 tháng của 1 năm (trả về Monthly, không phải Daily!)
        Task<List<MonthlyRevenueDto>> GetRevenueByYearAsync(int year);  // trả về 12 tháng

        // 5. Doanh thu nhiều năm (từ năm X đến năm Y)
        Task<List<MonthlyRevenueDto>> GetRevenueRangeAsync(int fromYear, int toYear);

        // 6. Tổng doanh thu từ trước đến nay
        Task<decimal> GetTotalRevenueAllTimeAsync();
    }
}
