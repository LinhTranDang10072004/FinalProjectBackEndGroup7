using HotelManagementApi.Data;
using HotelManagementApi.DTOs;
using HotelManagementApi.DTOs.Room;
using HotelManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Services
{
    public class RoomService : IRoomService
    {
        private readonly HotelDbContext _db;

        public RoomService(HotelDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RoomDto>> GetRooms()
        {
            return await _db.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.IsActive)
                .Select(r => new RoomDto
                {
                    RoomID = r.RoomID,
                    RoomNumber = r.RoomNumber,
                    Floor = r.Floor,
                    Status = r.Status,
                    IsActive = r.IsActive,
                    RoomTypeID = r.RoomTypeID,
                    RoomTypeName = r.RoomType.TypeName,
                    BasePrice = r.RoomType.BasePrice
                })
                .OrderBy(r => r.Floor)
                .ThenBy(r => r.RoomNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByStatus(string? status)
        {
            var query = _db.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.IsActive);

            // Nếu có status, filter theo status
            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(r => r.Status == status);
            }

            return await query
                .Select(r => new RoomDto
                {
                    RoomID = r.RoomID,
                    RoomNumber = r.RoomNumber,
                    Floor = r.Floor,
                    Status = r.Status,
                    IsActive = r.IsActive,
                    RoomTypeID = r.RoomTypeID,
                    RoomTypeName = r.RoomType.TypeName,
                    BasePrice = r.RoomType.BasePrice
                })
                .OrderBy(r => r.Floor)
                .ThenBy(r => r.RoomNumber)
                .ToListAsync();
        }

        public async Task<RoomDto?> GetRoom(int id)
        {
            var room = await _db.Rooms
                .Include(r => r.RoomType)
                .FirstOrDefaultAsync(r => r.RoomID == id);

            if (room == null) return null;

            return new RoomDto
            {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                Status = room.Status,
                IsActive = room.IsActive,
                RoomTypeID = room.RoomTypeID,
                RoomTypeName = room.RoomType.TypeName
            };
        }

        public async Task<RoomDto> CreateRoom(CreateRoomDto dto)
        {
            var room = new Room
            {
                RoomNumber = dto.RoomNumber,
                Floor = dto.Floor,
                RoomTypeID = dto.RoomTypeID,
                Status = "Available",
                IsActive = true
            };

            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();

            // Load room with RoomType to return DTO
            await _db.Entry(room).Reference(r => r.RoomType).LoadAsync();

            return new RoomDto
            {
                RoomID = room.RoomID,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                Status = room.Status,
                IsActive = room.IsActive,
                RoomTypeID = room.RoomTypeID,
                RoomTypeName = room.RoomType.TypeName,
                BasePrice = room.RoomType.BasePrice
            };
        }

        public async Task<bool> UpdateRoom(int id, UpdateRoomDto dto)
        {
            var room = await _db.Rooms.FindAsync(id);
            if (room == null) return false;

            if (dto.RoomNumber != null)
                room.RoomNumber = dto.RoomNumber;

            if (dto.Floor.HasValue)
                room.Floor = dto.Floor.Value;

            if (dto.IsActive.HasValue)
                room.IsActive = dto.IsActive.Value;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var room = await _db.Rooms
                .Include(r => r.BookingDetails)
                .FirstOrDefaultAsync(r => r.RoomID == id);
            
            if (room == null) return false;

            // Kiểm tra xem phòng có đang được sử dụng trong BookingDetails không
            if (room.BookingDetails != null && room.BookingDetails.Any())
            {
                throw new InvalidOperationException(
                    $"Không thể xóa phòng {room.RoomNumber} vì phòng này đã có lịch sử đặt phòng. " +
                    $"Có {room.BookingDetails.Count} booking detail(s) liên quan đến phòng này."
                );
            }

            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
