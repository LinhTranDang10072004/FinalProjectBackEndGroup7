using HotelManagementApi.DTOs.Reposts;

namespace HotelManagementApi.Services
{
    public interface IReportService
    {
        Task<decimal> GetRevenueByDateAsync(DateTime date);
        Task<List<DailyRevenueDto>> GetRevenueByMonthAsync(int year, int month);
        Task<List<DailyRevenueDto>> GetRevenueByYearAsync(int year);  // trả về 12 tháng
        Task<List<MonthlyRevenueDto>> GetRevenueRangeAsync(int fromYear, int toYear);

    }
}
