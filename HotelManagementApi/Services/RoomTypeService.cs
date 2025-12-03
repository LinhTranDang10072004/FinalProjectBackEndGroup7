using HotelManagementApi.Data;
using HotelManagementApi.DTOs.RoomType;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementApi.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly HotelDbContext _db;

        public RoomTypeService(HotelDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<RoomTypeDto>> GetRoomTypes()
        {
            return await _db.RoomTypes
                .Select(rt => new RoomTypeDto
                {
                    RoomTypeID = rt.RoomTypeID,
                    TypeName = rt.TypeName,
                    BasePrice = rt.BasePrice,
                    Capacity = rt.Capacity,
                    BedType = rt.BedType,
                    AreaSqm = rt.AreaSqm,
                    Description = rt.Description,
                    IsActive = rt.IsActive,
                    RoomCount = rt.Rooms.Count(r => r.IsActive)
                })
                .ToListAsync();
        }

        public async Task<RoomTypeDto?> GetRoomType(int id)
        {
            var roomType = await _db.RoomTypes
                .Include(rt => rt.Rooms)
                .FirstOrDefaultAsync(rt => rt.RoomTypeID == id);

            if (roomType == null) return null;

            return new RoomTypeDto
            {
                RoomTypeID = roomType.RoomTypeID,
                TypeName = roomType.TypeName,
                BasePrice = roomType.BasePrice,
                Capacity = roomType.Capacity,
                BedType = roomType.BedType,
                AreaSqm = roomType.AreaSqm,
                Description = roomType.Description,
                IsActive = roomType.IsActive,
                RoomCount = roomType.Rooms.Count(r => r.IsActive)
            };
        }

        public async Task<RoomTypeDto> CreateRoomType(CreateRoomTypeDto dto)
        {
            var roomType = new RoomType
            {
                TypeName = dto.TypeName,
                BasePrice = dto.BasePrice,
                Capacity = dto.Capacity,
                BedType = dto.BedType,
                AreaSqm = dto.AreaSqm,
                Description = dto.Description,
                IsActive = true
            };

            _db.RoomTypes.Add(roomType);
            await _db.SaveChangesAsync();

            return new RoomTypeDto
            {
                RoomTypeID = roomType.RoomTypeID,
                TypeName = roomType.TypeName,
                BasePrice = roomType.BasePrice,
                Capacity = roomType.Capacity,
                BedType = roomType.BedType,
                AreaSqm = roomType.AreaSqm,
                Description = roomType.Description,
                IsActive = roomType.IsActive,
                RoomCount = 0
            };
        }

        public async Task<bool> UpdateRoomType(int id, UpdateRoomTypeDto dto)
        {
            var roomType = await _db.RoomTypes.FindAsync(id);
            if (roomType == null) return false;

            roomType.TypeName = dto.TypeName;
            roomType.BasePrice = dto.BasePrice;
            roomType.Capacity = dto.Capacity;
            roomType.BedType = dto.BedType;
            roomType.AreaSqm = dto.AreaSqm;
            roomType.Description = dto.Description;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRoomType(int id)
        {
            var roomType = await _db.RoomTypes
                .Include(rt => rt.Rooms)
                .FirstOrDefaultAsync(rt => rt.RoomTypeID == id);

            if (roomType == null) return false;

            // Soft delete - set IsActive to false if there are rooms using this type
            if (roomType.Rooms.Any())
            {
                roomType.IsActive = false;
            }
            else
            {
                _db.RoomTypes.Remove(roomType);
            }

            await _db.SaveChangesAsync();
            return true;
        }
    }
}

