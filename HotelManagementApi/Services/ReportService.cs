using HotelManagementApi.Data;
using HotelManagementApi.DTOs.Reposts;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Services
{
    public class ReportService : IReportService
    {
        private readonly HotelDbContext _context;
        private const int TotalRooms = 20;

        public ReportService(HotelDbContext context) => _context = context;

        private IQueryable<BookingDetail> ValidBookings => _context.BookingDetails
            .Include(bd => bd.Booking)
            .Where(bd => bd.Booking.BookingStatus != "Cancelled" && bd.Booking.BookingStatus != "No-show");

        public Task<decimal> GetRevenueByDateAsync(DateTime date) =>
            GetRevenueByDateRangeAsync(date.Date, date.Date);

        public async Task<decimal> GetRevenueByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            var from = fromDate.Date;
            var to = toDate.Date.AddDays(1);

            var bookings = await ValidBookings
                .Where(bd => bd.CheckInDate < to && bd.CheckOutDate > from)
                .ToListAsync();

            decimal total = 0m;
            foreach (var bd in bookings)
            {
                var start = bd.CheckInDate.Date < from ? from : bd.CheckInDate.Date;
                var end = bd.CheckOutDate.Date > to ? to : bd.CheckOutDate.Date;
                var nights = (end - start).Days;
                if (nights > 0)
                    total += bd.PricePerNight * nights;
            }
            return total;
        }

        public async Task<List<DailyRevenueDto>> GetRevenueByMonthAsync(int year, int month)
        {
            var start = new DateTime(year, month, 1);
            var end = start.AddMonths(1);

            var dict = new Dictionary<DateTime, DailyRevenueDto>();
            for (var d = start; d < end; d = d.AddDays(1))
                dict[d.Date] = new DailyRevenueDto { Date = d };

            var bookings = await ValidBookings
                .Where(bd => bd.CheckInDate < end && bd.CheckOutDate > start)
                .ToListAsync();

            foreach (var bd in bookings)
            {
                for (var night = bd.CheckInDate.Date; night < bd.CheckOutDate.Date; night = night.AddDays(1))
                {
                    if (night >= start && night < end && dict.TryGetValue(night, out var dto))
                    {
                        dto.RoomRevenue += bd.PricePerNight;
                        dto.RoomNights += 1;
                        if (night.AddDays(1).Date == bd.CheckOutDate.Date)
                            dto.CheckOutCount += 1;
                    }
                }
            }

            foreach (var dto in dict.Values)
            {
                dto.OccupancyRate = Math.Round(dto.RoomNights / (decimal)TotalRooms * 100, 2);
                dto.ADR = dto.RoomNights > 0 ? Math.Round(dto.RoomRevenue / dto.RoomNights, 2) : 0;
                dto.RevPAR = Math.Round(dto.RoomRevenue / TotalRooms, 2);
            }

            return dict.Values.OrderBy(x => x.Date).ToList();
        }

        public async Task<List<MonthlyRevenueDto>> GetRevenueByYearAsync(int year)
        {
            var result = new List<MonthlyRevenueDto>();

            for (int m = 1; m <= 12; m++)
            {
                var monthStart = new DateTime(year, m, 1);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);
                var revenue = await GetRevenueByDateRangeAsync(monthStart, monthEnd);

                var days = DateTime.DaysInMonth(year, m);
                var dto = new MonthlyRevenueDto
                {
                    Year = year,
                    Month = m,
                    RoomRevenue = revenue,
                    TotalBookings = 0,
                    TotalRoomNights = 0,
                    AvgDailyRevenue = Math.Round(revenue / days, 2),
                    OccupancyRate = 0,
                    ADR = 0,
                    RevPAR = Math.Round(revenue / (TotalRooms * days), 2)
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<List<MonthlyRevenueDto>> GetRevenueRangeAsync(int fromYear, int toYear)
        {
            var list = new List<MonthlyRevenueDto>();
            for (int y = fromYear; y <= toYear; y++)
                list.AddRange(await GetRevenueByYearAsync(y));
            return list;
        }

        public async Task<decimal> GetTotalRevenueAllTimeAsync()
        {
            var bookings = await ValidBookings.ToListAsync();
            return bookings.Sum(bd => bd.PricePerNight * bd.Nights);
        }
    }
}