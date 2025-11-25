using HotelManagementApi.DTOs;
using HotelManagementApi.DTOs.Room;

namespace HotelManagementApi.Services
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetRooms();
        Task<RoomDto?> GetRoom(int id);
        Task<Room> CreateRoom(CreateRoomDto dto);
        Task<bool> UpdateRoom(int id, UpdateRoomDto dto);
        Task<bool> DeleteRoom(int id);
    }
}
