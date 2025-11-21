using Microsoft.EntityFrameworkCore;
using HotelManagementApi.Models;
using BCrypt.Net;

namespace HotelManagementApi.Data
{
    public static class SendData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Mật khẩu mặc định "123456" cho tất cả user mẫu
            var hasher = BCrypt.Net.BCrypt.HashPassword("123456");

            // ===================================================================
            // 1. Users - 1 Admin (Nguyễn Văn Quý) + 3 Receptionist + 16 Customer = 20 users
            // ===================================================================
            modelBuilder.Entity<User>().HasData(
                // Admin
                new User
                {
                    UserID = 1,
                    FirstName = "Nguyễn",
                    LastName = "Văn Quý",
                    Phone = "0901000001",
                    Email = "admin@hotel.com",
                    PasswordHash = hasher,
                    IdCard = "00123456789",
                    Address = "123 Đường Lê Lợi, Quận 1, TP.HCM",
                    DateOfBirth = new DateTime(1985, 3, 15),
                    Gender = "Male",
                    UserRole = "Admin",
                    Notes = "Quản trị viên hệ thống - Chủ khách sạn"
                },

                // 3 Receptionist
                new User { UserID = 2, FirstName = "Trần", LastName = "Thị Lan", Phone = "0901000002", Email = "lan@hotel.com", PasswordHash = hasher, IdCard = "00123456790", Address = "456 Hai Bà Trưng, Q3", DateOfBirth = new DateTime(1996, 7, 20), Gender = "Female", UserRole = "Receptionist", Notes = "Lễ tân ca sáng" },
                new User { UserID = 3, FirstName = "Lê", LastName = "Văn Hùng", Phone = "0901000003", Email = "hung@hotel.com", PasswordHash = hasher, IdCard = "00123456791", Address = "789 Nguyễn Huệ, Q1", DateOfBirth = new DateTime(1994, 11, 10), Gender = "Male", UserRole = "Receptionist", Notes = "Lễ tân ca tối" },
                new User { UserID = 4, FirstName = "Phạm", LastName = "Thị Mai", Phone = "0901000004", Email = "mai@hotel.com", PasswordHash = hasher, IdCard = "00123456792", Address = "321 Bùi Viện, Q1", DateOfBirth = new DateTime(1998, 4, 5), Gender = "Female", UserRole = "Receptionist", Notes = "Lễ tân ca đêm" },

                // 16 Customer (UserID 5 đến 20)
                new User { UserID = 5, FirstName = "Nguyễn", LastName = "Văn An", Phone = "0911000005", Email = "an@gmail.com", PasswordHash = hasher, IdCard = "00123456793", Address = "Hà Nội", DateOfBirth = new DateTime(1990, 1, 1), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 6, FirstName = "Trần", LastName = "Thị Bình", Phone = "0911000006", Email = "binh@gmail.com", PasswordHash = hasher, IdCard = "00123456794", Address = "Đà Nẵng", DateOfBirth = new DateTime(1992, 2, 2), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 7, FirstName = "Lê", LastName = "Văn Cường", Phone = "0911000007", Email = "cuong@gmail.com", PasswordHash = hasher, IdCard = "00123456795", Address = "Nha Trang", DateOfBirth = new DateTime(1988, 3, 3), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 8, FirstName = "Phạm", LastName = "Thị Dung", Phone = "0911000008", Email = "dung@gmail.com", PasswordHash = hasher, IdCard = "00123456796", Address = "Phú Quốc", DateOfBirth = new DateTime(1995, 4, 4), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 9, FirstName = "Hoàng", LastName = "Văn Em", Phone = "0911000009", Email = "em@gmail.com", PasswordHash = hasher, IdCard = "00123456797", Address = "Cần Thơ", DateOfBirth = new DateTime(1993, 5, 5), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 10, FirstName = "Vũ", LastName = "Thị Phượng", Phone = "0911000010", Email = "phuong@gmail.com", PasswordHash = hasher, IdCard = "00123456798", Address = "Hải Phòng", DateOfBirth = new DateTime(1991, 6, 6), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 11, FirstName = "Đặng", LastName = "Văn Giang", Phone = "0911000011", Email = "giang@gmail.com", PasswordHash = hasher, IdCard = "00123456799", Address = "Vũng Tàu", DateOfBirth = new DateTime(1989, 7, 7), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 12, FirstName = "Bùi", LastName = "Thị Hạnh", Phone = "0911000012", Email = "hanh@gmail.com", PasswordHash = hasher, IdCard = "00123456100", Address = "Đà Lạt", DateOfBirth = new DateTime(1997, 8, 8), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 13, FirstName = "Đỗ", LastName = "Văn Inh", Phone = "0911000013", Email = "inh@gmail.com", PasswordHash = hasher, IdCard = "00123456101", Address = "Huế", DateOfBirth = new DateTime(1994, 9, 9), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 14, FirstName = "Ngô", LastName = "Thị Kiều", Phone = "0911000014", Email = "kieu@gmail.com", PasswordHash = hasher, IdCard = "00123456102", Address = "Quảng Ninh", DateOfBirth = new DateTime(1996, 10, 10), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 15, FirstName = "Hà", LastName = "Văn Long", Phone = "0911000015", Email = "long@gmail.com", PasswordHash = hasher, IdCard = "00123456103", Address = "Sapa", DateOfBirth = new DateTime(1990, 11, 11), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 16, FirstName = "Lý", LastName = "Thị Minh", Phone = "0911000016", Email = "minh@gmail.com", PasswordHash = hasher, IdCard = "00123456104", Address = "Hội An", DateOfBirth = new DateTime(1992, 12, 12), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 17, FirstName = "Mai", LastName = "Văn Nam", Phone = "0911000017", Email = "nam@gmail.com", PasswordHash = hasher, IdCard = "00123456105", Address = "Ninh Bình", DateOfBirth = new DateTime(1987, 1, 13), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 18, FirstName = "Tô", LastName = "Thị Oanh", Phone = "0911000018", Email = "oanh@gmail.com", PasswordHash = hasher, IdCard = "00123456106", Address = "Phan Thiết", DateOfBirth = new DateTime(1999, 2, 14), Gender = "Female", UserRole = "Customer" },
                new User { UserID = 19, FirstName = "Dương", LastName = "Văn Phong", Phone = "0911000019", Email = "phong@gmail.com", PasswordHash = hasher, IdCard = "00123456107", Address = "Quy Nhơn", DateOfBirth = new DateTime(1993, 3, 15), Gender = "Male", UserRole = "Customer" },
                new User { UserID = 20, FirstName = "Chu", LastName = "Thị Quyên", Phone = "0911000020", Email = "quyen@gmail.com", PasswordHash = hasher, IdCard = "00123456108", Address = "Cà Mau", DateOfBirth = new DateTime(1995, 4, 16), Gender = "Female", UserRole = "Customer" }
            );

            // ===================================================================
            // 2. RoomType - 4 loại
            // ===================================================================
            modelBuilder.Entity<RoomType>().HasData(
                new RoomType { RoomTypeID = 1, TypeName = "Standard", BasePrice = 800000, Capacity = 2, BedType = "Double", AreaSqm = 25.00m, Description = "Phòng tiêu chuẩn" },
                new RoomType { RoomTypeID = 2, TypeName = "Deluxe", BasePrice = 1200000, Capacity = 2, BedType = "King", AreaSqm = 35.00m, Description = "Phòng cao cấp" },
                new RoomType { RoomTypeID = 3, TypeName = "Suite", BasePrice = 2500000, Capacity = 4, BedType = "2 Queen", AreaSqm = 60.00m, Description = "Phòng suite" },
                new RoomType { RoomTypeID = 4, TypeName = "Family", BasePrice = 1800000, Capacity = 4, BedType = "2 Double", AreaSqm = 45.00m, Description = "Phòng gia đình" }
            );

            // ===================================================================
            // 3. Room - 20 phòng (101 → 504)
            // ===================================================================
            modelBuilder.Entity<Room>().HasData(
                new Room { RoomID = 1, RoomNumber = "101", Floor = 1, RoomTypeID = 1, Status = "Available" },
                new Room { RoomID = 2, RoomNumber = "102", Floor = 1, RoomTypeID = 1, Status = "Occupied" },
                new Room { RoomID = 3, RoomNumber = "103", Floor = 1, RoomTypeID = 2, Status = "Available" },
                new Room { RoomID = 4, RoomNumber = "104", Floor = 1, RoomTypeID = 2, Status = "Occupied" },
                new Room { RoomID = 5, RoomNumber = "201", Floor = 2, RoomTypeID = 3, Status = "Available" },
                new Room { RoomID = 6, RoomNumber = "202", Floor = 2, RoomTypeID = 3, Status = "Occupied" },
                new Room { RoomID = 7, RoomNumber = "203", Floor = 2, RoomTypeID = 4, Status = "Available" },
                new Room { RoomID = 8, RoomNumber = "204", Floor = 2, RoomTypeID = 4, Status = "Available" },
                new Room { RoomID = 9, RoomNumber = "301", Floor = 3, RoomTypeID = 1, Status = "Maintenance" },
                new Room { RoomID = 10, RoomNumber = "302", Floor = 3, RoomTypeID = 1, Status = "Available" },
                new Room { RoomID = 11, RoomNumber = "303", Floor = 3, RoomTypeID = 2, Status = "Available" },
                new Room { RoomID = 12, RoomNumber = "304", Floor = 3, RoomTypeID = 2, Status = "Available" },
                new Room { RoomID = 13, RoomNumber = "401", Floor = 4, RoomTypeID = 3, Status = "Occupied" },
                new Room { RoomID = 14, RoomNumber = "402", Floor = 4, RoomTypeID = 3, Status = "Available" },
                new Room { RoomID = 15, RoomNumber = "403", Floor = 4, RoomTypeID = 4, Status = "Occupied" },
                new Room { RoomID = 16, RoomNumber = "404", Floor = 4, RoomTypeID = 4, Status = "Available" },
                new Room { RoomID = 17, RoomNumber = "501", Floor = 5, RoomTypeID = 1, Status = "Occupied" },
                new Room { RoomID = 18, RoomNumber = "502", Floor = 5, RoomTypeID = 2, Status = "Available" },
                new Room { RoomID = 19, RoomNumber = "503", Floor = 5, RoomTypeID = 3, Status = "Maintenance" },
                new Room { RoomID = 20, RoomNumber = "504", Floor = 5, RoomTypeID = 4, Status = "Available" }
            );

            // ===================================================================
            // 4. Booking + BookingDetail - 20 booking (đảm bảo không trùng phòng)
            // ===================================================================
            modelBuilder.Entity<Booking>().HasData(
                new Booking { BookingID = 1, BookingCode = "HTL25110001", UserID = 5, AdultCount = 2, ChildCount = 0, TotalAmount = 1600000, DepositAmount = 500000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 2, BookingCode = "HTL25110002", UserID = 6, AdultCount = 1, ChildCount = 1, TotalAmount = 2400000, DepositAmount = 2400000, BookingStatus = "Check-in" },
                new Booking { BookingID = 3, BookingCode = "HTL25110003", UserID = 7, AdultCount = 2, ChildCount = 0, TotalAmount = 3600000, DepositAmount = 0, BookingStatus = "Cancelled" },
                new Booking { BookingID = 4, BookingCode = "HTL25110004", UserID = 8, AdultCount = 2, ChildCount = 2, TotalAmount = 5000000, DepositAmount = 1000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 5, BookingCode = "HTL25110005", UserID = 9, AdultCount = 1, ChildCount = 0, TotalAmount = 1200000, DepositAmount = 1200000, BookingStatus = "Check-out" },
                new Booking { BookingID = 6, BookingCode = "HTL25110006", UserID = 10, AdultCount = 3, ChildCount = 1, TotalAmount = 7500000, DepositAmount = 2000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 7, BookingCode = "HTL25110007", UserID = 11, AdultCount = 2, ChildCount = 0, TotalAmount = 2400000, DepositAmount = 0, BookingStatus = "No-show" },
                new Booking { BookingID = 8, BookingCode = "HTL25110008", UserID = 12, AdultCount = 2, ChildCount = 0, TotalAmount = 1600000, DepositAmount = 800000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 9, BookingCode = "HTL25110009", UserID = 13, AdultCount = 4, ChildCount = 2, TotalAmount = 10000000, DepositAmount = 3000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 10, BookingCode = "HTL25110010", UserID = 14, AdultCount = 2, ChildCount = 0, TotalAmount = 3600000, DepositAmount = 3600000, BookingStatus = "Check-in" },
                new Booking { BookingID = 11, BookingCode = "HTL25110011", UserID = 15, AdultCount = 1, ChildCount = 0, TotalAmount = 1200000, DepositAmount = 0, BookingStatus = "Confirmed" },
                new Booking { BookingID = 12, BookingCode = "HTL25110012", UserID = 16, AdultCount = 2, ChildCount = 1, TotalAmount = 5000000, DepositAmount = 1500000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 13, BookingCode = "HTL25110013", UserID = 17, AdultCount = 3, ChildCount = 0, TotalAmount = 7500000, DepositAmount = 2000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 14, BookingCode = "HTL25110014", UserID = 18, AdultCount = 2, ChildCount = 0, TotalAmount = 2400000, DepositAmount = 2400000, BookingStatus = "Check-out" },
                new Booking { BookingID = 15, BookingCode = "HTL25110015", UserID = 19, AdultCount = 4, ChildCount = 2, TotalAmount = 12000000, DepositAmount = 4000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 16, BookingCode = "HTL25110016", UserID = 20, AdultCount = 2, ChildCount = 0, TotalAmount = 1600000, DepositAmount = 0, BookingStatus = "Confirmed" },
                new Booking { BookingID = 17, BookingCode = "HTL25110017", UserID = 5, AdultCount = 1, ChildCount = 1, TotalAmount = 3600000, DepositAmount = 1000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 18, BookingCode = "HTL25110018", UserID = 6, AdultCount = 2, ChildCount = 0, TotalAmount = 5000000, DepositAmount = 5000000, BookingStatus = "Check-in" },
                new Booking { BookingID = 19, BookingCode = "HTL25110019", UserID = 7, AdultCount = 3, ChildCount = 1, TotalAmount = 9000000, DepositAmount = 3000000, BookingStatus = "Confirmed" },
                new Booking { BookingID = 20, BookingCode = "HTL25110020", UserID = 8, AdultCount = 2, ChildCount = 0, TotalAmount = 2400000, DepositAmount = 2400000, BookingStatus = "Check-out" }
            );

            // BookingDetail (20+ dòng, đảm bảo không trùng phòng/ngày)
            modelBuilder.Entity<BookingDetail>().HasData(
                new BookingDetail { BookingDetailID = 1, BookingID = 1, RoomID = 1, RoomTypeID = 1, CheckInDate = new DateTime(2025, 12, 1, 14, 0, 0), CheckOutDate = new DateTime(2025, 12, 3, 12, 0, 0), PricePerNight = 800000 },
                new BookingDetail { BookingDetailID = 2, BookingID = 2, RoomID = 2, RoomTypeID = 1, CheckInDate = DateTime.Today.AddDays(-1), CheckOutDate = DateTime.Today.AddDays(2), PricePerNight = 800000 },
                new BookingDetail { BookingDetailID = 3, BookingID = 3, RoomID = 3, RoomTypeID = 2, CheckInDate = new DateTime(2025, 1, 5, 14, 0, 0), CheckOutDate = new DateTime(2025, 1, 8, 12, 0, 0), PricePerNight = 1200000 },
                new BookingDetail { BookingDetailID = 4, BookingID = 4, RoomID = 4, RoomTypeID = 2, CheckInDate = new DateTime(2025, 3, 10, 14, 0, 0), CheckOutDate = new DateTime(2025, 3, 12, 12, 0, 0), PricePerNight = 1200000 },
                new BookingDetail { BookingDetailID = 5, BookingID = 5, RoomID = 5, RoomTypeID = 3, CheckInDate = new DateTime(2025, 3, 15, 14, 0, 0), CheckOutDate = new DateTime(2025, 3, 18, 12, 0, 0), PricePerNight = 2500000 },
                new BookingDetail { BookingDetailID = 6, BookingID = 6, RoomID = 6, RoomTypeID = 3, CheckInDate = new DateTime(2025, 4, 20, 14, 0, 0), CheckOutDate = new DateTime(2025, 4, 22, 12, 0, 0), PricePerNight = 2500000 },
                new BookingDetail { BookingDetailID = 7, BookingID = 7, RoomID = 7, RoomTypeID = 4, CheckInDate = new DateTime(2025, 5, 25, 14, 0, 0), CheckOutDate = new DateTime(2025, 5, 27, 12, 0, 0), PricePerNight = 1800000 },
                new BookingDetail { BookingDetailID = 8, BookingID = 8, RoomID = 8, RoomTypeID = 4, CheckInDate = new DateTime(2025, 5, 28, 14, 0, 0), CheckOutDate = new DateTime(2025, 5, 30, 12, 0, 0), PricePerNight = 1800000 },
                new BookingDetail { BookingDetailID = 9, BookingID = 9, RoomID = 9, RoomTypeID = 1, CheckInDate = new DateTime(2025, 5, 1, 14, 0, 0), CheckOutDate = new DateTime(2025, 5, 5, 12, 0, 0), PricePerNight = 800000 },
                new BookingDetail { BookingDetailID = 10, BookingID = 10, RoomID = 10, RoomTypeID = 1, CheckInDate = new DateTime(2025, 6, 6, 14, 0, 0), CheckOutDate = new DateTime(2025, 6, 8, 12, 0, 0), PricePerNight = 800000 },
                new BookingDetail { BookingDetailID = 11, BookingID = 11, RoomID = 11, RoomTypeID = 2, CheckInDate = new DateTime(2025, 6, 9, 14, 0, 0), CheckOutDate = new DateTime(2025, 6, 11, 12, 0, 0), PricePerNight = 1200000 },
                new BookingDetail { BookingDetailID = 12, BookingID = 12, RoomID = 12, RoomTypeID = 2, CheckInDate = new DateTime(2025, 7, 12, 14, 0, 0), CheckOutDate = new DateTime(2025, 7, 14, 12, 0, 0), PricePerNight = 1200000 },
                new BookingDetail { BookingDetailID = 13, BookingID = 13, RoomID = 13, RoomTypeID = 3, CheckInDate = new DateTime(2025, 8, 15, 14, 0, 0), CheckOutDate = new DateTime(2025, 8, 17, 12, 0, 0), PricePerNight = 2500000 },
                new BookingDetail { BookingDetailID = 14, BookingID = 14, RoomID = 14, RoomTypeID = 3, CheckInDate = new DateTime(2025, 8, 18, 14, 0, 0), CheckOutDate = new DateTime(2025, 8, 20, 12, 0, 0), PricePerNight = 2500000 },
                new BookingDetail { BookingDetailID = 15, BookingID = 15, RoomID = 15, RoomTypeID = 4, CheckInDate = new DateTime(2025, 9, 21, 14, 0, 0), CheckOutDate = new DateTime(2025, 9, 24, 12, 0, 0), PricePerNight = 1800000 },
                new BookingDetail { BookingDetailID = 16, BookingID = 16, RoomID = 16, RoomTypeID = 4, CheckInDate = new DateTime(2025, 9, 25, 14, 0, 0), CheckOutDate = new DateTime(2025, 9, 27, 12, 0, 0), PricePerNight = 1800000 },
                new BookingDetail { BookingDetailID = 17, BookingID = 17, RoomID = 17, RoomTypeID = 1, CheckInDate = new DateTime(2025, 10, 28, 14, 0, 0), CheckOutDate = new DateTime(2025, 10, 30, 12, 0, 0), PricePerNight = 800000 },
                new BookingDetail { BookingDetailID = 18, BookingID = 18, RoomID = 18, RoomTypeID = 2, CheckInDate = new DateTime(2025, 11, 1, 14, 0, 0), CheckOutDate = new DateTime(2025, 11, 3, 12, 0, 0), PricePerNight = 1200000 },
                new BookingDetail { BookingDetailID = 19, BookingID = 19, RoomID = 19, RoomTypeID = 3, CheckInDate = new DateTime(2025, 11, 4, 14, 0, 0), CheckOutDate = new DateTime(2025, 11, 7, 12, 0, 0), PricePerNight = 2500000 },
                new BookingDetail { BookingDetailID = 20, BookingID = 20, RoomID = 20, RoomTypeID = 4, CheckInDate = new DateTime(2025, 12, 8, 14, 0, 0), CheckOutDate = new DateTime(2025, 12, 10, 12, 0, 0), PricePerNight = 1800000 }
            );


            // ===================================================================
            // 5. Invoice - 20 hóa đơn
            // ===================================================================
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice { InvoiceID = 1, BookingID = 1, InvoiceCode = "INV25110001", TotalRoomCharge = 1600000, PaidAmount = 500000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 2, BookingID = 2, InvoiceCode = "INV25110002", TotalRoomCharge = 2400000, PaidAmount = 2400000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 3, BookingID = 3, InvoiceCode = "INV25110003", TotalRoomCharge = 3600000, PaidAmount = 0, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 4, BookingID = 4, InvoiceCode = "INV25110004", TotalRoomCharge = 5000000, PaidAmount = 1000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 5, BookingID = 5, InvoiceCode = "INV25110005", TotalRoomCharge = 1200000, PaidAmount = 1200000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 6, BookingID = 6, InvoiceCode = "INV25110006", TotalRoomCharge = 7500000, PaidAmount = 2000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 7, BookingID = 7, InvoiceCode = "INV25110007", TotalRoomCharge = 2400000, PaidAmount = 0, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 8, BookingID = 8, InvoiceCode = "INV25110008", TotalRoomCharge = 1600000, PaidAmount = 800000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 9, BookingID = 9, InvoiceCode = "INV25110009", TotalRoomCharge = 10000000, PaidAmount = 3000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 10, BookingID = 10, InvoiceCode = "INV25110010", TotalRoomCharge = 3600000, PaidAmount = 3600000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 11, BookingID = 11, InvoiceCode = "INV25110011", TotalRoomCharge = 1200000, PaidAmount = 0, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 12, BookingID = 12, InvoiceCode = "INV25110012", TotalRoomCharge = 5000000, PaidAmount = 1500000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 13, BookingID = 13, InvoiceCode = "INV25110013", TotalRoomCharge = 7500000, PaidAmount = 2000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 14, BookingID = 14, InvoiceCode = "INV25110014", TotalRoomCharge = 2400000, PaidAmount = 2400000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 15, BookingID = 15, InvoiceCode = "INV25110015", TotalRoomCharge = 12000000, PaidAmount = 4000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 16, BookingID = 16, InvoiceCode = "INV25110016", TotalRoomCharge = 1600000, PaidAmount = 0, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 17, BookingID = 17, InvoiceCode = "INV25110017", TotalRoomCharge = 3600000, PaidAmount = 1000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 18, BookingID = 18, InvoiceCode = "INV25110018", TotalRoomCharge = 5000000, PaidAmount = 5000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 19, BookingID = 19, InvoiceCode = "INV25110019", TotalRoomCharge = 9000000, PaidAmount = 3000000, PaymentStatus = "Complete" },
                new Invoice { InvoiceID = 20, BookingID = 20, InvoiceCode = "INV25110020", TotalRoomCharge = 2400000, PaidAmount = 2400000, PaymentStatus = "Complete" }
            );

            // ===================================================================
            // 6. RoomStatusLog - 20 log
            // ===================================================================
            modelBuilder.Entity<RoomStatusLog>().HasData(
                new RoomStatusLog { LogID = 1, RoomID = 1, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-5), Reason = "Đặt phòng HTL25110001" },
                new RoomStatusLog { LogID = 2, RoomID = 2, OldStatus = "Available", NewStatus = "Occupied", ChangedBy = 3, ChangedDate = DateTime.Now.AddDays(-1), Reason = "Check-in khách walk-in" },
                new RoomStatusLog { LogID = 3, RoomID = 3, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 4, ChangedDate = DateTime.Now.AddDays(-3), Reason = "Đặt phòng HTL25110003" },
                new RoomStatusLog { LogID = 4, RoomID = 4, OldStatus = "Booked", NewStatus = "Occupied", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-2), Reason = "Check-in sớm" },
                new RoomStatusLog { LogID = 5, RoomID = 5, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 3, ChangedDate = DateTime.Now.AddDays(-4), Reason = "Đặt phòng HTL25110005" },
                new RoomStatusLog { LogID = 6, RoomID = 6, OldStatus = "Booked", NewStatus = "Occupied", ChangedBy = 4, ChangedDate = DateTime.Now.AddDays(-6), Reason = "Check-in" },
                new RoomStatusLog { LogID = 7, RoomID = 7, OldStatus = "Occupied", NewStatus = "Available", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-7), Reason = "Check-out" },
                new RoomStatusLog { LogID = 8, RoomID = 8, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 3, ChangedDate = DateTime.Now.AddDays(-8), Reason = "Đặt phòng HTL25110008" },
                new RoomStatusLog { LogID = 9, RoomID = 9, OldStatus = "Maintenance", NewStatus = "Available", ChangedBy = 1, ChangedDate = DateTime.Now.AddDays(-10), Reason = "Sửa chữa xong" },
                new RoomStatusLog { LogID = 10, RoomID = 10, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 4, ChangedDate = DateTime.Now.AddDays(-9), Reason = "Đặt phòng" },
                new RoomStatusLog { LogID = 11, RoomID = 11, OldStatus = "Booked", NewStatus = "Occupied", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-11), Reason = "Check-in" },
                new RoomStatusLog { LogID = 12, RoomID = 12, OldStatus = "Occupied", NewStatus = "Available", ChangedBy = 3, ChangedDate = DateTime.Now.AddDays(-12), Reason = "Check-out" },
                new RoomStatusLog { LogID = 13, RoomID = 13, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 4, ChangedDate = DateTime.Now.AddDays(-13), Reason = "Đặt phòng" },
                new RoomStatusLog { LogID = 14, RoomID = 14, OldStatus = "Booked", NewStatus = "Occupied", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-14), Reason = "Check-in" },
                new RoomStatusLog { LogID = 15, RoomID = 15, OldStatus = "Occupied", NewStatus = "Available", ChangedBy = 3, ChangedDate = DateTime.Now.AddDays(-15), Reason = "Check-out" },
                new RoomStatusLog { LogID = 16, RoomID = 16, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 4, ChangedDate = DateTime.Now.AddDays(-16), Reason = "Đặt phòng" },
                new RoomStatusLog { LogID = 17, RoomID = 17, OldStatus = "Booked", NewStatus = "Occupied", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-17), Reason = "Check-in" },
                new RoomStatusLog { LogID = 18, RoomID = 18, OldStatus = "Occupied", NewStatus = "Available", ChangedBy = 3, ChangedDate = DateTime.Now.AddDays(-18), Reason = "Check-out" },
                new RoomStatusLog { LogID = 19, RoomID = 19, OldStatus = "Available", NewStatus = "Booked", ChangedBy = 4, ChangedDate = DateTime.Now.AddDays(-19), Reason = "Đặt phòng" },
                new RoomStatusLog { LogID = 20, RoomID = 20, OldStatus = "Booked", NewStatus = "Occupied", ChangedBy = 2, ChangedDate = DateTime.Now.AddDays(-20), Reason = "Check-in" }
            );
        }
    }
}