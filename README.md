# FinalProjectBackEnd - Hotel Management API

## Tổng Quan
API backend cho hệ thống quản lý khách sạn được xây dựng bằng ASP.NET Core 8.0 với JWT Authentication.

## Tính Năng
- ✅ Xác thực JWT (Login/Logout)
- ✅ Quản lý Loại Phòng (Room Types) - CRUD với phân quyền Admin
- ✅ Quản lý Phòng (Rooms) - CRUD
- ✅ Báo cáo Doanh Thu (Revenue Reports) - Theo ngày/tháng/năm/khoảng thời gian
- ✅ Phân quyền theo Role (Admin, Receptionist, Customer)
- ✅ CORS được cấu hình cho Angular frontend
- ✅ Swagger UI cho testing

## Công Nghệ Sử Dụng
- ASP.NET Core 8.0
- Entity Framework Core
- SQL Server
- JWT Authentication
- BCrypt cho password hashing
- Swagger/OpenAPI

## Tài Liệu API

### Tiếng Việt
Xem file [DAC_TAC_API.md](./DAC_TAC_API.md) để biết đặc tả API chi tiết bằng tiếng Việt.

### English
See [API_SPECIFICATION.md](./API_SPECIFICATION.md) for detailed API specification in English.

## Cài Đặt và Chạy

1. **Cấu hình Database:**
   - Cập nhật connection string trong `appsettings.json`
   - Database sẽ tự động migrate khi ứng dụng khởi động

2. **Chạy ứng dụng:**
   ```bash
   dotnet run
   ```

3. **Truy cập Swagger:**
   - Development: `https://localhost:5001/swagger` hoặc `http://localhost:5000/swagger`

## Cấu Trúc Project

```
HotelManagementApi/
├── Controllers/          # API Controllers
│   ├── AuthController.cs
│   ├── RoomsController.cs
│   ├── RoomTypesController.cs
│   └── ReportsController.cs
├── Services/             # Business Logic
├── Models/              # Entity Models
├── DTOs/                # Data Transfer Objects
├── Data/                # DbContext và Migrations
└── Program.cs           # Startup configuration
```

## Endpoints Chính

- **Authentication:** `/api/auth/login`
- **Room Types:** `/api/roomtypes/*`
- **Rooms:** `/api/room/*`
- **Reports:** `/api/reports/revenue/*`

Xem chi tiết trong file đặc tả API.

## Lưu Ý

- JWT token có thời hạn 24 giờ
- Các endpoint quản lý Room Type yêu cầu quyền Admin
- CORS đã được cấu hình cho `http://localhost:4200` (Angular default port)