
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelManagementApi.Services;

namespace HotelManagementApi.Controllers
{
    [Route("api/reports/revenue")]
    [ApiController]
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

            return Ok(await _report.GetRevenueByYearAsync(year)); // cả năm
        }

        // GET: api/reports/revenue/yearly?from=2023&to=2025
        [HttpGet("yearly")]
        public async Task<IActionResult> GetYearly([FromQuery] int from, [FromQuery] int? to)
        {
            var toYear = to ?? DateTime.Today.Year;
            var data = await _report.GetRevenueRangeAsync(from, toYear);
            return Ok(data);
        }
    }
}
