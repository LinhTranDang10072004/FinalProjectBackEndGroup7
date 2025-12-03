using HotelManagementApi.DTOs.RoomType;
using HotelManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    [ApiController]
    [Route("api/roomtypes")]
    public class RoomTypesController : ControllerBase
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypesController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpGet("showAll")]
        public async Task<ActionResult> GetRoomTypes()
        {
            var roomTypes = await _roomTypeService.GetRoomTypes();
            return Ok(roomTypes);
        }

        [HttpGet("findRoomType/{id}")]
        public async Task<ActionResult> GetRoomType(int id)
        {
            var roomType = await _roomTypeService.GetRoomType(id);
            if (roomType == null) return NotFound();
            return Ok(roomType);
        }

        [HttpPost("CreateRoomType")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult> CreateRoomType(CreateRoomTypeDto dto)
        {
            var roomType = await _roomTypeService.CreateRoomType(dto);
            return CreatedAtAction(nameof(GetRoomType), new { id = roomType.RoomTypeID }, roomType);
        }

        [HttpPut("Update/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> UpdateRoomType(int id, UpdateRoomTypeDto dto)
        {
            var result = await _roomTypeService.UpdateRoomType(id, dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteRoomType(int id)
        {
            var result = await _roomTypeService.DeleteRoomType(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
