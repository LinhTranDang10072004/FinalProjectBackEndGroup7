# API Documentation - Room Management

## Base URL
```
https://localhost:7130/api/room
```

---

## 1. Lấy danh sách phòng đang hoạt động

**GET** `/showAll`

Lấy danh sách tất cả phòng đang hoạt động (IsActive = true)

### Response
```json
[
  {
    "roomID": 1,
    "roomNumber": "101",
    "floor": 1,
    "status": "Available",
    "isActive": true,
    "roomTypeID": 1,
    "roomTypeName": "Standard",
    "basePrice": 500000
  }
]
```

### Status Codes
- `200 OK`: Thành công

---

## 2. Lấy danh sách TẤT CẢ phòng (bao gồm cả không hoạt động)

**GET** `/showAllIncludingInactive`

Lấy danh sách tất cả phòng, bao gồm cả phòng không hoạt động (IsActive = false)

### Response
Tương tự như endpoint `/showAll`, nhưng bao gồm cả phòng có `isActive: false`

### Status Codes
- `200 OK`: Thành công

---

## 3. Lấy thông tin cơ bản một phòng

**GET** `/findRoom/{id}`

Lấy thông tin cơ bản của phòng (không bao gồm mô tả chi tiết, capacity, bed type, diện tích)

### Parameters
- `id` (path): ID của phòng

### Response
```json
{
  "roomID": 1,
  "roomNumber": "101",
  "floor": 1,
  "status": "Available",
  "isActive": true,
  "roomTypeID": 1,
  "roomTypeName": "Standard",
  "basePrice": 500000
}
```

### Status Codes
- `200 OK`: Tìm thấy phòng
- `404 Not Found`: Không tìm thấy phòng

---

## 4. Lấy thông tin CHI TIẾT đầy đủ một phòng ⭐

**GET** `/detail/{id}`

Lấy thông tin chi tiết đầy đủ của phòng bao gồm: giá, tầng, mô tả, sức chứa, loại giường, diện tích.

**Đây là endpoint chính để hiển thị detail khi người dùng nhấn vào một phòng.**

### Parameters
- `id` (path): ID của phòng (ví dụ: 101)

### Response
```json
{
  "roomID": 1,
  "roomNumber": "101",
  "floor": 1,
  "status": "Available",
  "isActive": true,
  "roomTypeID": 1,
  "roomTypeName": "Standard",
  "basePrice": 500000,
  "capacity": 2,
  "bedType": "Double",
  "areaSqm": 25.50,
  "description": "Phòng Standard với view đẹp, đầy đủ tiện nghi, wifi miễn phí, điều hòa, TV LCD 32 inch, minibar."
}
```

### Response Fields

| Field | Type | Mô tả |
|-------|------|-------|
| `roomID` | int | ID của phòng |
| `roomNumber` | string | Số phòng (ví dụ: "101") |
| `floor` | int | Tầng của phòng |
| `status` | string | Trạng thái: Available, Booked, Occupied, Maintenance |
| `isActive` | bool | Phòng có đang hoạt động không |
| `roomTypeID` | int | ID loại phòng |
| `roomTypeName` | string | Tên loại phòng (Standard, Deluxe, Suite...) |
| `basePrice` | decimal | Giá cơ bản mỗi đêm (VND) |
| `capacity` | int | Sức chứa (số người) |
| `bedType` | string | Loại giường (Single, Double, King Size...) |
| `areaSqm` | decimal | Diện tích phòng (m²) |
| `description` | string? | Mô tả chi tiết về phòng (có thể null) |

### Status Codes
- `200 OK`: Tìm thấy phòng và trả về thông tin chi tiết
- `404 Not Found`: Không tìm thấy phòng với ID này

### Example Request
```bash
GET /api/room/detail/101
```

### Example Response (Phòng 101)
```json
{
  "roomID": 1,
  "roomNumber": "101",
  "floor": 1,
  "status": "Available",
  "isActive": true,
  "roomTypeID": 1,
  "roomTypeName": "Standard",
  "basePrice": 500000,
  "capacity": 2,
  "bedType": "Double",
  "areaSqm": 25.50,
  "description": "Phòng Standard với view đẹp, đầy đủ tiện nghi, wifi miễn phí, điều hòa, TV LCD 32 inch, minibar."
}
```

### Khi nào sử dụng endpoint này?
- ✅ **Sử dụng**: Khi người dùng nhấn vào một phòng để xem chi tiết (ví dụ: click vào phòng 101)
- ✅ **Sử dụng**: Khi cần hiển thị đầy đủ thông tin phòng trong trang detail
- ❌ **Không sử dụng**: Khi chỉ cần danh sách phòng (dùng `/showAll` hoặc `/showAllIncludingInactive`)

---

## 5. Tạo phòng mới

**POST** `/CreateRoom`

### Request Body
```json
{
  "roomNumber": "201",
  "floor": 2,
  "roomTypeID": 1
}
```

### Response
```json
{
  "roomID": 10,
  "roomNumber": "201",
  "floor": 2,
  "status": "Available",
  "isActive": true,
  "roomTypeID": 1,
  "roomTypeName": "Standard",
  "basePrice": 500000
}
```

### Status Codes
- `201 Created`: Tạo phòng thành công
- `400 Bad Request`: Dữ liệu đầu vào không hợp lệ

---

## 6. Cập nhật thông tin phòng

**PUT** `/Update/{id}`

Cập nhật thông tin phòng (số phòng, tầng, trạng thái hoạt động, status)

### Parameters
- `id` (path): ID của phòng

### Request Body
Tất cả các trường đều optional (có thể null). Chỉ các trường được gửi lên mới được cập nhật.

```json
{
  "roomNumber": "202",        // Optional: Số phòng mới (tối đa 10 ký tự)
  "floor": 2,                 // Optional: Tầng mới (1-100)
  "isActive": true,           // Optional: Trạng thái hoạt động
  "status": "Maintenance"     // Optional: Trạng thái phòng
}
```

### Các giá trị Status hợp lệ:
- `"Available"`: Phòng trống, sẵn sàng đặt
- `"Booked"`: Phòng đã được đặt
- `"Occupied"`: Phòng đang có khách
- `"Maintenance"`: Phòng đang bảo trì

### Response
Không có body

### Status Codes
- `204 No Content`: Cập nhật thành công
- `400 Bad Request`: Dữ liệu không hợp lệ (ví dụ: status sai định dạng)
- `404 Not Found`: Không tìm thấy phòng

### Example: Đổi status sang Maintenance
```bash
PUT /api/room/Update/20
Content-Type: application/json

{
  "status": "Maintenance"
}
```

---

## 7. Cập nhật trạng thái phòng (Status)

**PATCH** `/UpdateStatus/{id}`

Chỉ cập nhật status của phòng (chuyên dụng cho việc đổi status)

### Parameters
- `id` (path): ID của phòng

### Request Body
```json
{
  "status": "Maintenance"
}
```

### Các giá trị Status hợp lệ:
- `"Available"`: Phòng trống, sẵn sàng đặt
- `"Booked"`: Phòng đã được đặt
- `"Occupied"`: Phòng đang có khách
- `"Maintenance"`: Phòng đang bảo trì

### Response
Không có body

### Status Codes
- `204 No Content`: Cập nhật thành công
- `400 Bad Request`: Status không hợp lệ
- `404 Not Found`: Không tìm thấy phòng

### Example: Đổi phòng sang bảo trì
```bash
PATCH /api/room/UpdateStatus/20
Content-Type: application/json

{
  "status": "Maintenance"
}
```

---

## 8. Xóa phòng

**DELETE** `/Delete/{id}`

Xóa phòng (chỉ xóa được nếu phòng chưa có lịch sử đặt phòng)

### Parameters
- `id` (path): ID của phòng

### Response
Không có body

### Status Codes
- `204 No Content`: Xóa thành công
- `400 Bad Request`: Không thể xóa vì phòng đã có lịch sử đặt phòng
- `404 Not Found`: Không tìm thấy phòng
- `500 Internal Server Error`: Lỗi server

### Error Response (400)
```json
{
  "message": "Không thể xóa phòng 101 vì phòng này đã có lịch sử đặt phòng. Có 5 booking detail(s) liên quan đến phòng này."
}
```

---

## Tóm tắt các Status

| Status | Mô tả | Khi nào sử dụng |
|--------|-------|-----------------|
| `Available` | Phòng trống, sẵn sàng đặt | Phòng đang trống, chưa có ai đặt |
| `Booked` | Phòng đã được đặt | Khách đã đặt phòng nhưng chưa check-in |
| `Occupied` | Phòng đang có khách | Khách đã check-in và đang ở trong phòng |
| `Maintenance` | Phòng đang bảo trì | Phòng cần sửa chữa, dọn dẹp, không thể sử dụng |

---

## Ví dụ sử dụng với JavaScript/Fetch

### Lấy tất cả phòng (bao gồm cả không hoạt động)
```javascript
fetch('https://localhost:7130/api/room/showAllIncludingInactive')
  .then(response => response.json())
  .then(data => console.log(data));
```

### Lấy thông tin chi tiết phòng (Khi người dùng nhấn vào phòng 101)
```javascript
// Lấy detail phòng 101
fetch('https://localhost:7130/api/room/detail/101')
  .then(response => {
    if (!response.ok) {
      throw new Error('Không tìm thấy phòng');
    }
    return response.json();
  })
  .then(roomDetail => {
    console.log('Thông tin chi tiết phòng:', roomDetail);
    console.log('Số phòng:', roomDetail.roomNumber);
    console.log('Tầng:', roomDetail.floor);
    console.log('Giá:', roomDetail.basePrice.toLocaleString('vi-VN') + ' VND');
    console.log('Sức chứa:', roomDetail.capacity, 'người');
    console.log('Loại giường:', roomDetail.bedType);
    console.log('Diện tích:', roomDetail.areaSqm, 'm²');
    console.log('Mô tả:', roomDetail.description);
  })
  .catch(error => console.error('Lỗi:', error));
```

### Lấy thông tin chi tiết phòng với async/await (Recommended)
```javascript
async function getRoomDetail(roomId) {
  try {
    const response = await fetch(`https://localhost:7130/api/room/detail/${roomId}`);
    
    if (!response.ok) {
      if (response.status === 404) {
        throw new Error('Không tìm thấy phòng');
      }
      throw new Error('Lỗi khi lấy thông tin phòng');
    }
    
    const roomDetail = await response.json();
    
    // Hiển thị thông tin phòng
    return {
      roomNumber: roomDetail.roomNumber,
      floor: roomDetail.floor,
      price: roomDetail.basePrice,
      capacity: roomDetail.capacity,
      bedType: roomDetail.bedType,
      area: roomDetail.areaSqm,
      description: roomDetail.description,
      status: roomDetail.status,
      roomType: roomDetail.roomTypeName
    };
  } catch (error) {
    console.error('Lỗi:', error);
    return null;
  }
}

// Sử dụng: Lấy detail phòng 101
const room101 = await getRoomDetail(101);
if (room101) {
  console.log('Phòng:', room101.roomNumber);
  console.log('Tầng:', room101.floor);
  console.log('Giá:', room101.price.toLocaleString('vi-VN') + ' VND');
}
```

### Đổi status phòng sang Maintenance
```javascript
fetch('https://localhost:7130/api/room/UpdateStatus/20', {
  method: 'PATCH',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    status: 'Maintenance'
  })
})
.then(response => {
  if (response.ok) {
    console.log('Đổi status thành công');
  } else {
    return response.json().then(err => console.error(err));
  }
});
```

### Cập nhật thông tin phòng (có thể đổi status)
```javascript
fetch('https://localhost:7130/api/room/Update/20', {
  method: 'PUT',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    status: 'Maintenance',
    isActive: false  // Có thể kết hợp với việc tắt hoạt động
  })
})
.then(response => {
  if (response.ok) {
    console.log('Cập nhật thành công');
  } else {
    return response.json().then(err => console.error(err));
  }
});
```

---

## Lưu ý

1. **Lấy phòng không hoạt động**: Sử dụng endpoint `/showAllIncludingInactive` thay vì `/showAll`
2. **Xem chi tiết phòng**: 
   - **Sử dụng `GET /detail/{id}`** khi người dùng nhấn vào một phòng để xem detail
   - Endpoint này trả về đầy đủ thông tin: giá, tầng, mô tả, capacity, bed type, diện tích
   - Ví dụ: `GET /api/room/detail/101` để xem chi tiết phòng 101
3. **Đổi status**: Có 2 cách:
   - Sử dụng `PATCH /UpdateStatus/{id}` - Chỉ đổi status
   - Sử dụng `PUT /Update/{id}` - Có thể đổi nhiều thông tin cùng lúc
4. **Validation**: Status phải là một trong 4 giá trị: `Available`, `Booked`, `Occupied`, `Maintenance`
5. **Xóa phòng**: Chỉ xóa được phòng chưa có booking history. Nếu phòng đã có booking, sẽ trả về lỗi 400 với thông báo rõ ràng.

---

## Tóm tắt nhanh các endpoint chính

| Endpoint | Method | Mô tả | Khi nào dùng |
|----------|--------|-------|--------------|
| `/showAll` | GET | Lấy danh sách phòng đang hoạt động | Hiển thị danh sách phòng |
| `/showAllIncludingInactive` | GET | Lấy tất cả phòng (kể cả không hoạt động) | Quản lý phòng, cần xem cả phòng đã tắt |
| `/detail/{id}` | GET | **Lấy chi tiết đầy đủ một phòng** ⭐ | **Khi người dùng nhấn vào phòng để xem detail** |
| `/findRoom/{id}` | GET | Lấy thông tin cơ bản phòng | Khi chỉ cần thông tin cơ bản |
| `/UpdateStatus/{id}` | PATCH | Đổi status phòng | Đổi status (ví dụ: sang Maintenance) |
| `/Update/{id}` | PUT | Cập nhật thông tin phòng | Cập nhật nhiều thông tin cùng lúc |
| `/Delete/{id}` | DELETE | Xóa phòng | Xóa phòng (nếu chưa có booking) |


