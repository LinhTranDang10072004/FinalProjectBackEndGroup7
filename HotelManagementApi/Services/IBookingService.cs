using HotelManagementApi.DTOs.Booking;

namespace HotelManagementApi.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingDto>> GetBookings();
        Task<IEnumerable<BookingDto>> GetBookingsByStatus(string? status);
        Task<IEnumerable<BookingDto>> GetBookingsByUser(int userId);
        Task<BookingDto?> GetBooking(int id);
        Task<BookingDto?> GetBookingByCode(string bookingCode);
        Task<BookingDto> CreateBooking(CreateBookingDto dto);
        Task<bool> UpdateBookingStatus(int id, UpdateBookingStatusDto dto);
        Task<bool> CancelBooking(int id, string? cancelReason);
        Task<bool> DeleteBooking(int id);
    }
}



