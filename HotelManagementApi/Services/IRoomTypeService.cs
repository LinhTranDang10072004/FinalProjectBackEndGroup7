using HotelManagementApi.DTOs.RoomType;

namespace HotelManagementApi.Services
{
    public interface IRoomTypeService
    {
        Task<IEnumerable<RoomTypeDto>> GetRoomTypes();
        Task<RoomTypeDto?> GetRoomType(int id);
        Task<RoomTypeDto> CreateRoomType(CreateRoomTypeDto dto);
        Task<bool> UpdateRoomType(int id, UpdateRoomTypeDto dto);
        Task<bool> DeleteRoomType(int id);
    }
}
