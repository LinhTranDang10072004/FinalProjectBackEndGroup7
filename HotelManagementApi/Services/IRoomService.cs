using HotelManagementApi.DTOs;
using HotelManagementApi.DTOs.Room;

namespace HotelManagementApi.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetRooms();
        Task<IEnumerable<RoomDto>> GetAllRooms();
        Task<IEnumerable<RoomDto>> GetRoomsByStatus(string? status);
        Task<RoomDto?> GetRoom(int id);
        Task<RoomDetailDto?> GetRoomDetail(int id);
        Task<RoomDto> CreateRoom(CreateRoomDto dto);
        Task<bool> UpdateRoom(int id, UpdateRoomDto dto);
        Task<bool> UpdateRoomStatus(int id, string status);
        Task<bool> DeleteRoom(int id);
    }
}
