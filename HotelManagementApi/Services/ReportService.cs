using HotelManagementApi.Data;
using HotelManagementApi.DTOs.Reposts;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Services
{
    public class ReportService : IReportService
    {
        private readonly HotelDbContext _context;

        public ReportService(HotelDbContext context)
        {
            _context = context;
        }

        // 1. Doanh thu 1 ngày (dựa vào ngày Check-out)
        public async Task<decimal> GetRevenueByDateAsync(DateTime date)
        {
            var start = date.Date;
            var end = date.Date.AddDays(1);

            return await _context.BookingDetails
                .Where(bd => bd.CheckOutDate >= start && bd.CheckOutDate < end)
                .SumAsync(bd => bd.PricePerNight * EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date));
        }

        // 2. Chi tiết từng ngày trong 1 tháng (bao gồm ngày 0đ)
        public async Task<List<DailyRevenueDto>> GetRevenueByMonthAsync(int year, int month)
        {
            var startOfMonth = new DateTime(year, month, 1);
            var endOfMonth = startOfMonth.AddMonths(1);

            var rawData = await _context.BookingDetails
                .Where(bd => bd.CheckOutDate >= startOfMonth && bd.CheckOutDate < endOfMonth)
                .GroupBy(bd => bd.CheckOutDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Revenue = g.Sum(bd => bd.PricePerNight * EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date)),
                    Count = g.Count(),
                    Nights = g.Sum(bd => EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date))
                })
                .ToListAsync();

            // Tạo đầy đủ các ngày trong tháng (kể cả ngày không có doanh thu)
            var allDays = Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                .Select(d => new DateTime(year, month, d))
                .ToList();

            var result = allDays.Select(day =>
            {
                var data = rawData.FirstOrDefault(x => x.Date == day.Date);
                return new DailyRevenueDto
                {
                    Date = day.Date,
                    RoomRevenue = data?.Revenue ?? 0m,
                    CheckOutCount = data?.Count ?? 0,
                    RoomNights = data?.Nights ?? 0
                };
            }).ToList();

            return result;
        }

        // 3. Doanh thu cả năm (12 tháng)
        public async Task<List<DailyRevenueDto>> GetRevenueByYearAsync(int year)
        {
            var start = new DateTime(year, 1, 1);
            var end = new DateTime(year + 1, 1, 1);

            var monthlyData = await _context.BookingDetails
                .Where(bd => bd.CheckOutDate >= start && bd.CheckOutDate < end)
                .GroupBy(bd => new { bd.CheckOutDate.Year, bd.CheckOutDate.Month })
                .Select(g => new DailyRevenueDto
                {
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1),
                    RoomRevenue = g.Sum(bd => bd.PricePerNight * EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date)),
                    CheckOutCount = g.Count(),
                    RoomNights = g.Sum(bd => EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date))
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            return monthlyData;
        }

        // 4. Doanh thu nhiều năm
        public async Task<List<MonthlyRevenueDto>> GetRevenueRangeAsync(int fromYear, int toYear)
        {
            var start = new DateTime(fromYear, 1, 1);
            var end = new DateTime(toYear + 1, 1, 1);

            return await _context.BookingDetails
                .Where(bd => bd.CheckOutDate >= start && bd.CheckOutDate < end)
                .GroupBy(bd => new { bd.CheckOutDate.Year, bd.CheckOutDate.Month })
                .Select(g => new MonthlyRevenueDto
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    RoomRevenue = g.Sum(bd => bd.PricePerNight * EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date)),
                    TotalBookings = g.Select(bd => bd.BookingID).Distinct().Count(),
                    TotalRoomNights = g.Sum(bd => EF.Functions.DateDiffDay(bd.CheckInDate.Date, bd.CheckOutDate.Date))
                })
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToListAsync();
        }
    }
}