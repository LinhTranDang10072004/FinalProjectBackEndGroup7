using HotelManagementApi.DTOs.Room;
using HotelManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    [ApiController]
    [Route("api/room")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("showAll")]
        public async Task<ActionResult> GetRooms()
        {
            var rooms = await _roomService.GetRooms();
            return Ok(rooms);
        }

        [HttpGet("findRoom/{id}")]
        public async Task<ActionResult> GetRoom(int id)
        {
            var room = await _roomService.GetRoom(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost("CreateRoom")]
        public async Task<ActionResult> CreateRoom(CreateRoomDto dto)
        {
            var room = await _roomService.CreateRoom(dto);
            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomID }, room);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDto dto)
        {
            var result = await _roomService.UpdateRoom(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var result = await _roomService.DeleteRoom(id);
            if (!result) return NotFound();
            return NoContent();
        }

}
