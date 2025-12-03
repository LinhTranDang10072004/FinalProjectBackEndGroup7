using HotelManagementApi.Data;
using HotelManagementApi.DTOs.Booking;
using HotelManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Services
{
    public class BookingService : IBookingService
    {
        private readonly HotelDbContext _db;

        public BookingService(HotelDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<BookingDto>> GetBookings()
        {
            return await _db.Bookings
                .Include(b => b.User)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.RoomType)
                .OrderByDescending(b => b.CreatedDate)
                .Select(b => new BookingDto
                {
                    BookingID = b.BookingID,
                    BookingCode = b.BookingCode,
                    UserID = b.UserID,
                    UserName = $"{b.User.FirstName} {b.User.LastName}".Trim(),
                    UserEmail = b.User.Email,
                    AdultCount = b.AdultCount,
                    ChildCount = b.ChildCount,
                    TotalAmount = b.TotalAmount,
                    DepositAmount = b.DepositAmount,
                    BookingStatus = b.BookingStatus,
                    Notes = b.Notes,
                    CreatedDate = b.CreatedDate,
                    CancelReason = b.CancelReason,
                    CancelDate = b.CancelDate,
                    BookingDetails = b.BookingDetails.Select(bd => new BookingDetailDto
                    {
                        BookingDetailID = bd.BookingDetailID,
                        RoomID = bd.RoomID,
                        RoomNumber = bd.Room.RoomNumber,
                        RoomTypeID = bd.RoomTypeID,
                        RoomTypeName = bd.RoomType.TypeName,
                        CheckInDate = bd.CheckInDate,
                        CheckOutDate = bd.CheckOutDate,
                        Nights = (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days,
                        PricePerNight = bd.PricePerNight,
                        TotalCharge = bd.PricePerNight * (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByStatus(string? status)
        {
            var query = _db.Bookings
                .Include(b => b.User)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.RoomType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(b => b.BookingStatus == status);
            }

            return await query
                .OrderByDescending(b => b.CreatedDate)
                .Select(b => new BookingDto
                {
                    BookingID = b.BookingID,
                    BookingCode = b.BookingCode,
                    UserID = b.UserID,
                    UserName = $"{b.User.FirstName} {b.User.LastName}".Trim(),
                    UserEmail = b.User.Email,
                    AdultCount = b.AdultCount,
                    ChildCount = b.ChildCount,
                    TotalAmount = b.TotalAmount,
                    DepositAmount = b.DepositAmount,
                    BookingStatus = b.BookingStatus,
                    Notes = b.Notes,
                    CreatedDate = b.CreatedDate,
                    CancelReason = b.CancelReason,
                    CancelDate = b.CancelDate,
                    BookingDetails = b.BookingDetails.Select(bd => new BookingDetailDto
                    {
                        BookingDetailID = bd.BookingDetailID,
                        RoomID = bd.RoomID,
                        RoomNumber = bd.Room.RoomNumber,
                        RoomTypeID = bd.RoomTypeID,
                        RoomTypeName = bd.RoomType.TypeName,
                        CheckInDate = bd.CheckInDate,
                        CheckOutDate = bd.CheckOutDate,
                        Nights = (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days,
                        PricePerNight = bd.PricePerNight,
                        TotalCharge = bd.PricePerNight * (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<BookingDto>> GetBookingsByUser(int userId)
        {
            return await _db.Bookings
                .Include(b => b.User)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.RoomType)
                .Where(b => b.UserID == userId)
                .OrderByDescending(b => b.CreatedDate)
                .Select(b => new BookingDto
                {
                    BookingID = b.BookingID,
                    BookingCode = b.BookingCode,
                    UserID = b.UserID,
                    UserName = $"{b.User.FirstName} {b.User.LastName}".Trim(),
                    UserEmail = b.User.Email,
                    AdultCount = b.AdultCount,
                    ChildCount = b.ChildCount,
                    TotalAmount = b.TotalAmount,
                    DepositAmount = b.DepositAmount,
                    BookingStatus = b.BookingStatus,
                    Notes = b.Notes,
                    CreatedDate = b.CreatedDate,
                    CancelReason = b.CancelReason,
                    CancelDate = b.CancelDate,
                    BookingDetails = b.BookingDetails.Select(bd => new BookingDetailDto
                    {
                        BookingDetailID = bd.BookingDetailID,
                        RoomID = bd.RoomID,
                        RoomNumber = bd.Room.RoomNumber,
                        RoomTypeID = bd.RoomTypeID,
                        RoomTypeName = bd.RoomType.TypeName,
                        CheckInDate = bd.CheckInDate,
                        CheckOutDate = bd.CheckOutDate,
                        Nights = (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days,
                        PricePerNight = bd.PricePerNight,
                        TotalCharge = bd.PricePerNight * (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<BookingDto?> GetBooking(int id)
        {
            var booking = await _db.Bookings
                .Include(b => b.User)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.RoomType)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null) return null;

            return new BookingDto
            {
                BookingID = booking.BookingID,
                BookingCode = booking.BookingCode,
                UserID = booking.UserID,
                UserName = $"{booking.User.FirstName} {booking.User.LastName}".Trim(),
                UserEmail = booking.User.Email,
                AdultCount = booking.AdultCount,
                ChildCount = booking.ChildCount,
                TotalAmount = booking.TotalAmount,
                DepositAmount = booking.DepositAmount,
                BookingStatus = booking.BookingStatus,
                Notes = booking.Notes,
                CreatedDate = booking.CreatedDate,
                CancelReason = booking.CancelReason,
                CancelDate = booking.CancelDate,
                BookingDetails = booking.BookingDetails.Select(bd => new BookingDetailDto
                {
                    BookingDetailID = bd.BookingDetailID,
                    RoomID = bd.RoomID,
                    RoomNumber = bd.Room.RoomNumber,
                    RoomTypeID = bd.RoomTypeID,
                    RoomTypeName = bd.RoomType.TypeName,
                    CheckInDate = bd.CheckInDate,
                    CheckOutDate = bd.CheckOutDate,
                    Nights = (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days,
                    PricePerNight = bd.PricePerNight,
                    TotalCharge = bd.PricePerNight * (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days
                }).ToList()
            };
        }

        public async Task<BookingDto?> GetBookingByCode(string bookingCode)
        {
            var booking = await _db.Bookings
                .Include(b => b.User)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.Room)
                .Include(b => b.BookingDetails)
                    .ThenInclude(bd => bd.RoomType)
                .FirstOrDefaultAsync(b => b.BookingCode == bookingCode);

            if (booking == null) return null;

            return new BookingDto
            {
                BookingID = booking.BookingID,
                BookingCode = booking.BookingCode,
                UserID = booking.UserID,
                UserName = $"{booking.User.FirstName} {booking.User.LastName}".Trim(),
                UserEmail = booking.User.Email,
                AdultCount = booking.AdultCount,
                ChildCount = booking.ChildCount,
                TotalAmount = booking.TotalAmount,
                DepositAmount = booking.DepositAmount,
                BookingStatus = booking.BookingStatus,
                Notes = booking.Notes,
                CreatedDate = booking.CreatedDate,
                CancelReason = booking.CancelReason,
                CancelDate = booking.CancelDate,
                BookingDetails = booking.BookingDetails.Select(bd => new BookingDetailDto
                {
                    BookingDetailID = bd.BookingDetailID,
                    RoomID = bd.RoomID,
                    RoomNumber = bd.Room.RoomNumber,
                    RoomTypeID = bd.RoomTypeID,
                    RoomTypeName = bd.RoomType.TypeName,
                    CheckInDate = bd.CheckInDate,
                    CheckOutDate = bd.CheckOutDate,
                    Nights = (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days,
                    PricePerNight = bd.PricePerNight,
                    TotalCharge = bd.PricePerNight * (bd.CheckOutDate.Date - bd.CheckInDate.Date).Days
                }).ToList()
            };
        }

        public async Task<BookingDto> CreateBooking(CreateBookingDto dto)
        {
            // Generate booking code
            var bookingCode = GenerateBookingCode();

            // Validate rooms availability
            foreach (var detail in dto.BookingDetails)
            {
                var room = await _db.Rooms.FindAsync(detail.RoomID);
                if (room == null)
                    throw new ArgumentException($"Room with ID {detail.RoomID} not found");

                // Check if room is available for the dates
                var hasConflict = await _db.BookingDetails
                    .Include(bd => bd.Booking)
                    .AnyAsync(bd => bd.RoomID == detail.RoomID
                        && bd.Booking.BookingStatus != "Cancelled"
                        && bd.Booking.BookingStatus != "No-show"
                        && bd.CheckInDate < detail.CheckOutDate
                        && bd.CheckOutDate > detail.CheckInDate);

                if (hasConflict)
                    throw new InvalidOperationException($"Room {room.RoomNumber} is not available for the selected dates");

                if (room.Status != "Available" && room.Status != "Booked")
                    throw new InvalidOperationException($"Room {room.RoomNumber} is not available (Status: {room.Status})");
            }

            // Create booking
            var booking = new Booking
            {
                BookingCode = bookingCode,
                UserID = dto.UserID,
                AdultCount = dto.AdultCount,
                ChildCount = dto.ChildCount,
                TotalAmount = dto.TotalAmount,
                DepositAmount = dto.DepositAmount,
                BookingStatus = "Confirmed",
                Notes = dto.Notes,
                CreatedDate = DateTime.Now
            };

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            // Create booking details
            foreach (var detailDto in dto.BookingDetails)
            {
                var bookingDetail = new BookingDetail
                {
                    BookingID = booking.BookingID,
                    RoomID = detailDto.RoomID,
                    RoomTypeID = detailDto.RoomTypeID,
                    CheckInDate = detailDto.CheckInDate.Date,
                    CheckOutDate = detailDto.CheckOutDate.Date,
                    PricePerNight = detailDto.PricePerNight
                };

                _db.BookingDetails.Add(bookingDetail);

                // Update room status to Booked
                var room = await _db.Rooms.FindAsync(detailDto.RoomID);
                if (room != null)
                {
                    room.Status = "Booked";
                }
            }

            await _db.SaveChangesAsync();

            // Return created booking
            return await GetBooking(booking.BookingID) ?? throw new Exception("Failed to retrieve created booking");
        }

        public async Task<bool> UpdateBookingStatus(int id, UpdateBookingStatusDto dto)
        {
            var booking = await _db.Bookings
                .Include(b => b.BookingDetails)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null) return false;

            var oldStatus = booking.BookingStatus;
            booking.BookingStatus = dto.BookingStatus;

            if (dto.BookingStatus == "Cancelled")
            {
                booking.CancelReason = dto.CancelReason;
                booking.CancelDate = DateTime.Now;

                // Update room status back to Available
                foreach (var detail in booking.BookingDetails)
                {
                    var room = await _db.Rooms.FindAsync(detail.RoomID);
                    if (room != null)
                    {
                        room.Status = "Available";
                    }
                }
            }
            else if (dto.BookingStatus == "Check-in")
            {
                // Update room status to Occupied
                foreach (var detail in booking.BookingDetails)
                {
                    var room = await _db.Rooms.FindAsync(detail.RoomID);
                    if (room != null)
                    {
                        room.Status = "Occupied";
                    }
                }
            }
            else if (dto.BookingStatus == "Check-out")
            {
                // Update room status back to Available
                foreach (var detail in booking.BookingDetails)
                {
                    var room = await _db.Rooms.FindAsync(detail.RoomID);
                    if (room != null)
                    {
                        room.Status = "Available";
                    }
                }
            }

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CancelBooking(int id, string? cancelReason)
        {
            var dto = new UpdateBookingStatusDto
            {
                BookingStatus = "Cancelled",
                CancelReason = cancelReason
            };
            return await UpdateBookingStatus(id, dto);
        }

        public async Task<bool> DeleteBooking(int id)
        {
            var booking = await _db.Bookings
                .Include(b => b.BookingDetails)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null) return false;

            // Update room status back to Available
            foreach (var detail in booking.BookingDetails)
            {
                var room = await _db.Rooms.FindAsync(detail.RoomID);
                if (room != null)
                {
                    room.Status = "Available";
                }
            }

            _db.Bookings.Remove(booking);
            await _db.SaveChangesAsync();
            return true;
        }

        private string GenerateBookingCode()
        {
            var prefix = "HTL";
            var date = DateTime.Now.ToString("yyMMdd");
            var random = new Random().Next(1000, 9999);
            return $"{prefix}{date}{random}";
        }
    }
}



