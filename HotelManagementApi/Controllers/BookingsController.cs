using HotelManagementApi.DTOs.Booking;
using HotelManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    [ApiController]
    [Route("api/booking")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// Lấy danh sách tất cả booking
        /// </summary>
        [HttpGet("showAll")]
        public async Task<ActionResult> GetBookings([FromQuery] string? status = null)
        {
            IEnumerable<BookingDto> bookings;

            if (!string.IsNullOrWhiteSpace(status))
            {
                bookings = await _bookingService.GetBookingsByStatus(status);
            }
            else
            {
                bookings = await _bookingService.GetBookings();
            }

            return Ok(bookings);
        }

        /// <summary>
        /// Lấy booking theo ID
        /// </summary>
        [HttpGet("findBooking/{id}")]
        public async Task<ActionResult> GetBooking(int id)
        {
            var booking = await _bookingService.GetBooking(id);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Lấy booking theo mã booking code
        /// </summary>
        [HttpGet("findByCode/{bookingCode}")]
        public async Task<ActionResult> GetBookingByCode(string bookingCode)
        {
            var booking = await _bookingService.GetBookingByCode(bookingCode);
            if (booking == null) return NotFound();
            return Ok(booking);
        }

        /// <summary>
        /// Lấy booking theo User ID
        /// </summary>
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult> GetBookingsByUser(int userId)
        {
            var bookings = await _bookingService.GetBookingsByUser(userId);
            return Ok(bookings);
        }

        /// <summary>
        /// Tạo booking mới
        /// </summary>
        [HttpPost("CreateBooking")]
        public async Task<ActionResult> CreateBooking(CreateBookingDto dto)
        {
            try
            {
                var booking = await _bookingService.CreateBooking(dto);
                return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingID }, booking);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating booking", error = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật trạng thái booking
        /// </summary>
        [HttpPut("UpdateStatus/{id}")]
        [Authorize(Policy = "StaffOnly")]
        public async Task<IActionResult> UpdateBookingStatus(int id, UpdateBookingStatusDto dto)
        {
            var result = await _bookingService.UpdateBookingStatus(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Hủy booking
        /// </summary>
        [HttpPut("Cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id, [FromBody] string? cancelReason = null)
        {
            var result = await _bookingService.CancelBooking(id, cancelReason);
            if (!result) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Xóa booking (chỉ khi booking đã cancelled hoặc no-show)
        /// </summary>
        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var result = await _bookingService.DeleteBooking(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

