using HotelManagementApi.DTOs;
using HotelManagementApi.DTOs.Room;

namespace HotelManagementApi.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetRooms();
        Task<IEnumerable<RoomDto>> GetRoomsByStatus(string? status);
        Task<RoomDto?> GetRoom(int id);
        Task<RoomDto> CreateRoom(CreateRoomDto dto);
        Task<bool> UpdateRoom(int id, UpdateRoomDto dto);
        Task<bool> DeleteRoom(int id);
    }
}
