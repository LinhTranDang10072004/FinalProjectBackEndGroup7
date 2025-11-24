using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagementApi.Services;

namespace HotelManagementApi.Controllers
{
    [ApiController]
    [Route("api/reports/revenue")]  
    //[Authorize(Roles = "Admin,Receptionist")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _report;

        public ReportsController(IReportService report) => _report = report;

        // GET: api/reports/revenue/daily?date=2025-12-03
        [HttpGet("daily")]
        public async Task<IActionResult> GetDaily([FromQuery] DateTime date)
        {
            var revenue = await _report.GetRevenueByDateAsync(date);
            return Ok(new { Date = date.ToString("yyyy-MM-dd"), Revenue = revenue });
        }

        // GET: api/reports/revenue/monthly?year=2025&month=12
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly([FromQuery] int year, [FromQuery] int? month)
        {
            if (month.HasValue)
                return Ok(await _report.GetRevenueByMonthAsync(year, month.Value));

            // Nếu không có month → trả về 12 tháng của năm
            return Ok(await _report.GetRevenueByYearAsync(year));
        }

        // GET: api/reports/revenue/yearly?from=2023&to=2025
        [HttpGet("yearly")]
        public async Task<IActionResult> GetYearly([FromQuery] int from, [FromQuery] int? to)
        {
            var toYear = to ?? DateTime.Today.Year;
            var data = await _report.GetRevenueRangeAsync(from, toYear);
            return Ok(data);
        }

        //Từ ngày đến ngày bất kỳ
        [HttpGet("range")]
        public async Task<IActionResult> GetRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var revenue = await _report.GetRevenueByDateRangeAsync(from, to);
            return Ok(new
            {
                From = from.ToString("yyyy-MM-dd"),
                To = to.ToString("yyyy-MM-dd"),
                TotalRevenue = revenue
            });
        }

        //Tổng doanh thu từ trước đến nay
        [HttpGet("total")]
        public async Task<IActionResult> GetTotal()
        {
            var total = await _report.GetTotalRevenueAllTimeAsync();
            return Ok(new { TotalRevenueAllTime = total });
        }

    }
}