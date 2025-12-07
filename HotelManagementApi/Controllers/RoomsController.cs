using HotelManagementApi.DTOs;
using HotelManagementApi.DTOs.Room;
using HotelManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementApi.Controllers
{
    /// <summary>
    /// Controller quản lý phòng khách sạn
    /// </summary>
    [ApiController]
    [Route("api/room")]
    [Produces("application/json")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Lấy danh sách tất cả phòng đang hoạt động (IsActive = true)
        /// </summary>
        /// <returns>Danh sách phòng đang hoạt động, sắp xếp theo tầng và số phòng</returns>
        /// <response code="200">Trả về danh sách phòng thành công</response>
        [HttpGet("showAll")]
        [ProducesResponseType(typeof(IEnumerable<RoomDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetRooms()
        {
            var rooms = await _roomService.GetRooms();
            return Ok(rooms);
        }

        /// <summary>
        /// Lấy danh sách TẤT CẢ phòng (bao gồm cả phòng không hoạt động)
        /// </summary>
        /// <returns>Danh sách tất cả phòng, bao gồm cả phòng không hoạt động (IsActive = false)</returns>
        /// <response code="200">Trả về danh sách tất cả phòng thành công</response>
        [HttpGet("showAllIncludingInactive")]
        [ProducesResponseType(typeof(IEnumerable<RoomDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            return Ok(rooms);
        }

        /// <summary>
        /// Lấy thông tin cơ bản một phòng theo ID
        /// </summary>
        /// <param name="id">ID của phòng cần tìm</param>
        /// <returns>Thông tin cơ bản phòng</returns>
        /// <response code="200">Tìm thấy phòng</response>
        /// <response code="404">Không tìm thấy phòng với ID này</response>
        [HttpGet("findRoom/{id}")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRoom(int id)
        {
            var room = await _roomService.GetRoom(id);
            if (room == null) return NotFound(new { message = $"Không tìm thấy phòng với ID {id}" });
            return Ok(room);
        }

        /// <summary>
        /// Lấy thông tin CHI TIẾT đầy đủ của một phòng theo ID (bao gồm giá, tầng, mô tả, capacity, bed type, diện tích)
        /// </summary>
        /// <param name="id">ID của phòng cần xem chi tiết</param>
        /// <returns>Thông tin chi tiết đầy đủ của phòng</returns>
        /// <response code="200">Tìm thấy phòng và trả về thông tin chi tiết</response>
        /// <response code="404">Không tìm thấy phòng với ID này</response>
        /// <remarks>
        /// Endpoint này trả về đầy đủ thông tin chi tiết của phòng bao gồm:
        /// - Thông tin cơ bản: số phòng, tầng, status, isActive
        /// - Thông tin loại phòng: tên loại, giá, sức chứa, loại giường, diện tích
        /// - Mô tả chi tiết về phòng
        /// 
        /// Ví dụ: GET /api/room/detail/101 sẽ trả về thông tin chi tiết của phòng 101
        /// </remarks>
        [HttpGet("detail/{id}")]
        [ProducesResponseType(typeof(RoomDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetRoomDetail(int id)
        {
            var roomDetail = await _roomService.GetRoomDetail(id);
            if (roomDetail == null) return NotFound(new { message = $"Không tìm thấy phòng với ID {id}" });
            return Ok(roomDetail);
        }

        /// <summary>
        /// Tạo phòng mới
        /// </summary>
        /// <param name="dto">Thông tin phòng mới cần tạo</param>
        /// <returns>Thông tin phòng vừa được tạo</returns>
        /// <response code="201">Tạo phòng thành công</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        [HttpPost("CreateRoom")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateRoom(CreateRoomDto dto)
        {
            var room = await _roomService.CreateRoom(dto);
            return CreatedAtAction(nameof(GetRoom), new { id = room.RoomID }, room);
        }

        /// <summary>
        /// Cập nhật thông tin phòng (số phòng, tầng, trạng thái hoạt động, status)
        /// </summary>
        /// <param name="id">ID của phòng cần cập nhật</param>
        /// <param name="dto">Thông tin cần cập nhật (các trường null sẽ không được cập nhật)</param>
        /// <returns>Không có nội dung trả về</returns>
        /// <response code="204">Cập nhật thành công</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ (ví dụ: status không đúng định dạng)</response>
        /// <response code="404">Không tìm thấy phòng với ID này</response>
        /// <remarks>
        /// Các trường có thể cập nhật:
        /// - RoomNumber: Số phòng (tối đa 10 ký tự)
        /// - Floor: Tầng (1-100)
        /// - IsActive: Trạng thái hoạt động (true/false)
        /// - Status: Trạng thái phòng - phải là một trong: 'Available', 'Booked', 'Occupied', 'Maintenance'
        /// </remarks>
        [HttpPut("Update/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRoom(int id, UpdateRoomDto dto)
        {
            try
            {
                var result = await _roomService.UpdateRoom(id, dto);
                if (!result) return NotFound(new { message = $"Không tìm thấy phòng với ID {id}" });
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật trạng thái phòng (Available, Booked, Occupied, Maintenance)
        /// </summary>
        /// <param name="id">ID của phòng cần cập nhật status</param>
        /// <param name="dto">DTO chứa status mới</param>
        /// <returns>Không có nội dung trả về</returns>
        /// <response code="204">Cập nhật status thành công</response>
        /// <response code="400">Status không hợp lệ hoặc dữ liệu đầu vào không đúng</response>
        /// <response code="404">Không tìm thấy phòng với ID này</response>
        /// <remarks>
        /// Các giá trị status hợp lệ:
        /// - Available: Phòng trống, sẵn sàng đặt
        /// - Booked: Phòng đã được đặt
        /// - Occupied: Phòng đang có khách
        /// - Maintenance: Phòng đang bảo trì
        /// </remarks>
        [HttpPatch("UpdateStatus/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRoomStatus(int id, [FromBody] UpdateRoomStatusDto dto)
        {
            try
            {
                var result = await _roomService.UpdateRoomStatus(id, dto.Status);
                if (!result) return NotFound(new { message = $"Không tìm thấy phòng với ID {id}" });
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Xóa phòng (chỉ xóa được nếu phòng chưa có lịch sử đặt phòng)
        /// </summary>
        /// <param name="id">ID của phòng cần xóa</param>
        /// <returns>Không có nội dung trả về</returns>
        /// <response code="204">Xóa phòng thành công</response>
        /// <response code="400">Không thể xóa phòng vì phòng đã có lịch sử đặt phòng</response>
        /// <response code="404">Không tìm thấy phòng với ID này</response>
        /// <response code="500">Lỗi server khi xóa phòng</response>
        [HttpDelete("Delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            try
            {
                var result = await _roomService.DeleteRoom(id);
                if (!result) return NotFound(new { message = $"Không tìm thấy phòng với ID {id}" });
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi xóa phòng.", error = ex.Message });
            }
        }

    }
}
