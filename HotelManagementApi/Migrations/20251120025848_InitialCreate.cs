using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    BedType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AreaSqm = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.RoomTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IdCard = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Nationality = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Other"),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Customer")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.CheckConstraint("CK_Users_Gender", "Gender IN ('Male', 'Female', 'Other')");
                    table.CheckConstraint("CK_Users_Role", "UserRole IN ('Admin', 'Receptionist', 'Customer')");
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Available"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomID);
                    table.CheckConstraint("CK_Room_Status", "Status IN ('Available', 'Booked', 'Occupied', 'Maintenance')");
                    table.ForeignKey(
                        name: "FK_Room_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingCode = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    AdultCount = table.Column<int>(type: "int", nullable: false),
                    ChildCount = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookingStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Confirmed"),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CancelReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CancelDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingID);
                    table.CheckConstraint("CK_Booking_Status", "BookingStatus IN ('Confirmed', 'Check-in', 'Check-out', 'Cancelled', 'No-show')");
                    table.ForeignKey(
                        name: "FK_Booking_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoomStatusLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OldStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangedBy = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    RoomID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomStatusLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_RoomStatusLogs_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomStatusLogs_Users_ChangedBy",
                        column: x => x.ChangedBy,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    RoomTypeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingDetailID);
                    table.CheckConstraint("CK_BookingDetail_DateRange", "CheckOutDate > CheckInDate");
                    table.CheckConstraint("CK_BookingDetail_Price", "PricePerNight > 0");
                    table.ForeignKey(
                        name: "FK_BookingDetails_Booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingDetails_RoomTypes_RoomTypeID",
                        column: x => x.RoomTypeID,
                        principalTable: "RoomTypes",
                        principalColumn: "RoomTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Room_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Room",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalRoomCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalServiceCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VATPercent = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, defaultValue: "Complete"),
                    BookingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceID);
                    table.CheckConstraint("CK_Invoice_PaymentStatus", "PaymentStatus IN ('Complete')");
                    table.ForeignKey(
                        name: "FK_Invoice_Booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "RoomTypeID", "AreaSqm", "BasePrice", "BedType", "Capacity", "Description", "IsActive", "TypeName" },
                values: new object[,]
                {
                    { 1, 25.00m, 800000m, "Double", 2, "Phòng tiêu chuẩn", true, "Standard" },
                    { 2, 35.00m, 1200000m, "King", 2, "Phòng cao cấp", true, "Deluxe" },
                    { 3, 60.00m, 2500000m, "2 Queen", 4, "Phòng suite", true, "Suite" },
                    { 4, 45.00m, 1800000m, "2 Double", 4, "Phòng gia đình", true, "Family" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "CreatedDate", "DateOfBirth", "Email", "FirstName", "Gender", "IdCard", "LastName", "Nationality", "Notes", "PasswordHash", "Phone", "UserRole" },
                values: new object[,]
                {
                    { 1, "123 Đường Lê Lợi, Quận 1, TP.HCM", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9418), new DateTime(1985, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@hotel.com", "Nguyễn", "Male", "00123456789", "Văn Quý", "VietNam", "Quản trị viên hệ thống - Chủ khách sạn", "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0901000001", "Admin" },
                    { 2, "456 Hai Bà Trưng, Q3", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9465), new DateTime(1996, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "lan@hotel.com", "Trần", "Female", "00123456790", "Thị Lan", "VietNam", "Lễ tân ca sáng", "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0901000002", "Receptionist" },
                    { 3, "789 Nguyễn Huệ, Q1", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9468), new DateTime(1994, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "hung@hotel.com", "Lê", "Male", "00123456791", "Văn Hùng", "VietNam", "Lễ tân ca tối", "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0901000003", "Receptionist" },
                    { 4, "321 Bùi Viện, Q1", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9471), new DateTime(1998, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "mai@hotel.com", "Phạm", "Female", "00123456792", "Thị Mai", "VietNam", "Lễ tân ca đêm", "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0901000004", "Receptionist" },
                    { 5, "Hà Nội", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9474), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "an@gmail.com", "Nguyễn", "Male", "00123456793", "Văn An", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000005", "Customer" },
                    { 6, "Đà Nẵng", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9477), new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "binh@gmail.com", "Trần", "Female", "00123456794", "Thị Bình", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000006", "Customer" },
                    { 7, "Nha Trang", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9479), new DateTime(1988, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "cuong@gmail.com", "Lê", "Male", "00123456795", "Văn Cường", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000007", "Customer" },
                    { 8, "Phú Quốc", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9481), new DateTime(1995, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "dung@gmail.com", "Phạm", "Female", "00123456796", "Thị Dung", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000008", "Customer" },
                    { 9, "Cần Thơ", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9482), new DateTime(1993, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "em@gmail.com", "Hoàng", "Male", "00123456797", "Văn Em", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000009", "Customer" },
                    { 10, "Hải Phòng", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9484), new DateTime(1991, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "phuong@gmail.com", "Vũ", "Female", "00123456798", "Thị Phượng", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000010", "Customer" },
                    { 11, "Vũng Tàu", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9486), new DateTime(1989, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "giang@gmail.com", "Đặng", "Male", "00123456799", "Văn Giang", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000011", "Customer" },
                    { 12, "Đà Lạt", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9626), new DateTime(1997, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "hanh@gmail.com", "Bùi", "Female", "00123456100", "Thị Hạnh", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000012", "Customer" },
                    { 13, "Huế", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9629), new DateTime(1994, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "inh@gmail.com", "Đỗ", "Male", "00123456101", "Văn Inh", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000013", "Customer" },
                    { 14, "Quảng Ninh", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9631), new DateTime(1996, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "kieu@gmail.com", "Ngô", "Female", "00123456102", "Thị Kiều", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000014", "Customer" },
                    { 15, "Sapa", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9634), new DateTime(1990, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "long@gmail.com", "Hà", "Male", "00123456103", "Văn Long", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000015", "Customer" },
                    { 16, "Hội An", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9636), new DateTime(1992, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "minh@gmail.com", "Lý", "Female", "00123456104", "Thị Minh", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000016", "Customer" },
                    { 17, "Ninh Bình", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9638), new DateTime(1987, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "nam@gmail.com", "Mai", "Male", "00123456105", "Văn Nam", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000017", "Customer" },
                    { 18, "Phan Thiết", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9640), new DateTime(1999, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "oanh@gmail.com", "Tô", "Female", "00123456106", "Thị Oanh", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000018", "Customer" },
                    { 19, "Quy Nhơn", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9642), new DateTime(1993, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "phong@gmail.com", "Dương", "Male", "00123456107", "Văn Phong", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000019", "Customer" },
                    { 20, "Cà Mau", new DateTime(2025, 11, 20, 9, 58, 48, 191, DateTimeKind.Local).AddTicks(9643), new DateTime(1995, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "quyen@gmail.com", "Chu", "Female", "00123456108", "Thị Quyên", "VietNam", null, "$2a$11$ST7TQ.V6xB5pIRV.jnWtFuMmbPa6Iog4ojzuHqMkC8YafWePJ.fxi", "0911000020", "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "BookingID", "AdultCount", "BookingCode", "BookingStatus", "CancelDate", "CancelReason", "ChildCount", "CreatedDate", "DepositAmount", "Notes", "TotalAmount", "UserID" },
                values: new object[,]
                {
                    { 1, 2, "HTL25110001", "Confirmed", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(270), 500000m, null, 1600000m, 5 },
                    { 2, 1, "HTL25110002", "Check-in", null, null, 1, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(277), 2400000m, null, 2400000m, 6 },
                    { 3, 2, "HTL25110003", "Cancelled", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(279), 0m, null, 3600000m, 7 },
                    { 4, 2, "HTL25110004", "Confirmed", null, null, 2, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(282), 1000000m, null, 5000000m, 8 },
                    { 5, 1, "HTL25110005", "Check-out", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(316), 1200000m, null, 1200000m, 9 },
                    { 6, 3, "HTL25110006", "Confirmed", null, null, 1, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(318), 2000000m, null, 7500000m, 10 },
                    { 7, 2, "HTL25110007", "No-show", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(319), 0m, null, 2400000m, 11 },
                    { 8, 2, "HTL25110008", "Confirmed", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(321), 800000m, null, 1600000m, 12 },
                    { 9, 4, "HTL25110009", "Confirmed", null, null, 2, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(323), 3000000m, null, 10000000m, 13 },
                    { 10, 2, "HTL25110010", "Check-in", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(324), 3600000m, null, 3600000m, 14 },
                    { 11, 1, "HTL25110011", "Confirmed", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(326), 0m, null, 1200000m, 15 },
                    { 12, 2, "HTL25110012", "Confirmed", null, null, 1, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(327), 1500000m, null, 5000000m, 16 },
                    { 13, 3, "HTL25110013", "Confirmed", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(330), 2000000m, null, 7500000m, 17 },
                    { 14, 2, "HTL25110014", "Check-out", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(332), 2400000m, null, 2400000m, 18 },
                    { 15, 4, "HTL25110015", "Confirmed", null, null, 2, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(334), 4000000m, null, 12000000m, 19 },
                    { 16, 2, "HTL25110016", "Confirmed", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(335), 0m, null, 1600000m, 20 },
                    { 17, 1, "HTL25110017", "Confirmed", null, null, 1, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(337), 1000000m, null, 3600000m, 5 },
                    { 18, 2, "HTL25110018", "Check-in", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(338), 5000000m, null, 5000000m, 6 },
                    { 19, 3, "HTL25110019", "Confirmed", null, null, 1, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(340), 3000000m, null, 9000000m, 7 },
                    { 20, 2, "HTL25110020", "Check-out", null, null, 0, new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(341), 2400000m, null, 2400000m, 8 }
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "RoomID", "Floor", "IsActive", "RoomNumber", "RoomTypeID", "Status" },
                values: new object[,]
                {
                    { 1, 1, true, "101", 1, "Available" },
                    { 2, 1, true, "102", 1, "Occupied" },
                    { 3, 1, true, "103", 2, "Available" },
                    { 4, 1, true, "104", 2, "Occupied" },
                    { 5, 2, true, "201", 3, "Available" },
                    { 6, 2, true, "202", 3, "Occupied" },
                    { 7, 2, true, "203", 4, "Available" },
                    { 8, 2, true, "204", 4, "Available" },
                    { 9, 3, true, "301", 1, "Maintenance" },
                    { 10, 3, true, "302", 1, "Available" },
                    { 11, 3, true, "303", 2, "Available" },
                    { 12, 3, true, "304", 2, "Available" },
                    { 13, 4, true, "401", 3, "Occupied" },
                    { 14, 4, true, "402", 3, "Available" },
                    { 15, 4, true, "403", 4, "Occupied" },
                    { 16, 4, true, "404", 4, "Available" },
                    { 17, 5, true, "501", 1, "Occupied" },
                    { 18, 5, true, "502", 2, "Available" },
                    { 19, 5, true, "503", 3, "Maintenance" },
                    { 20, 5, true, "504", 4, "Available" }
                });

            migrationBuilder.InsertData(
                table: "BookingDetails",
                columns: new[] { "BookingDetailID", "BookingID", "CheckInDate", "CheckOutDate", "PricePerNight", "RoomID", "RoomTypeID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 12, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 800000m, 1, 1 },
                    { 2, 2, new DateTime(2025, 11, 19, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 11, 22, 0, 0, 0, 0, DateTimeKind.Local), 800000m, 2, 1 },
                    { 3, 3, new DateTime(2025, 12, 5, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), 1200000m, 3, 2 },
                    { 4, 4, new DateTime(2025, 12, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), 1200000m, 4, 2 },
                    { 5, 5, new DateTime(2025, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), 2500000m, 5, 3 },
                    { 6, 6, new DateTime(2025, 12, 20, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 2500000m, 6, 3 },
                    { 7, 7, new DateTime(2025, 12, 25, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1800000m, 7, 4 },
                    { 8, 8, new DateTime(2025, 12, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), 1800000m, 8, 4 },
                    { 9, 9, new DateTime(2025, 12, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), 800000m, 9, 1 },
                    { 10, 10, new DateTime(2025, 12, 6, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), 800000m, 10, 1 },
                    { 11, 11, new DateTime(2025, 12, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 1200000m, 11, 2 },
                    { 12, 12, new DateTime(2025, 12, 12, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), 1200000m, 12, 2 },
                    { 13, 13, new DateTime(2025, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 2500000m, 13, 3 },
                    { 14, 14, new DateTime(2025, 12, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 2500000m, 14, 3 },
                    { 15, 15, new DateTime(2025, 12, 21, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 1800000m, 15, 4 },
                    { 16, 16, new DateTime(2025, 12, 25, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 1800000m, 16, 4 },
                    { 17, 17, new DateTime(2025, 12, 28, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), 800000m, 17, 1 },
                    { 18, 18, new DateTime(2025, 12, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 1200000m, 18, 2 },
                    { 19, 19, new DateTime(2025, 12, 4, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 2500000m, 19, 3 },
                    { 20, 20, new DateTime(2025, 12, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 1800000m, 20, 4 }
                });

            migrationBuilder.InsertData(
                table: "Invoice",
                columns: new[] { "InvoiceID", "BookingID", "InvoiceCode", "IssueDate", "PaidAmount", "PaymentStatus", "TotalDiscount", "TotalRoomCharge", "TotalServiceCharge", "VATPercent" },
                values: new object[,]
                {
                    { 1, 1, "INV25110001", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(501), 500000m, "Complete", 0m, 1600000m, 0m, 10m },
                    { 2, 2, "INV25110002", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(504), 2400000m, "Complete", 0m, 2400000m, 0m, 10m },
                    { 3, 3, "INV25110003", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(505), 0m, "Complete", 0m, 3600000m, 0m, 10m },
                    { 4, 4, "INV25110004", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(508), 1000000m, "Complete", 0m, 5000000m, 0m, 10m },
                    { 5, 5, "INV25110005", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(509), 1200000m, "Complete", 0m, 1200000m, 0m, 10m },
                    { 6, 6, "INV25110006", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(511), 2000000m, "Complete", 0m, 7500000m, 0m, 10m },
                    { 7, 7, "INV25110007", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(512), 0m, "Complete", 0m, 2400000m, 0m, 10m },
                    { 8, 8, "INV25110008", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(513), 800000m, "Complete", 0m, 1600000m, 0m, 10m },
                    { 9, 9, "INV25110009", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(514), 3000000m, "Complete", 0m, 10000000m, 0m, 10m },
                    { 10, 10, "INV25110010", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(516), 3600000m, "Complete", 0m, 3600000m, 0m, 10m },
                    { 11, 11, "INV25110011", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(519), 0m, "Complete", 0m, 1200000m, 0m, 10m },
                    { 12, 12, "INV25110012", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(520), 1500000m, "Complete", 0m, 5000000m, 0m, 10m },
                    { 13, 13, "INV25110013", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(538), 2000000m, "Complete", 0m, 7500000m, 0m, 10m },
                    { 14, 14, "INV25110014", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(539), 2400000m, "Complete", 0m, 2400000m, 0m, 10m },
                    { 15, 15, "INV25110015", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(541), 4000000m, "Complete", 0m, 12000000m, 0m, 10m },
                    { 16, 16, "INV25110016", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(542), 0m, "Complete", 0m, 1600000m, 0m, 10m },
                    { 17, 17, "INV25110017", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(545), 1000000m, "Complete", 0m, 3600000m, 0m, 10m },
                    { 18, 18, "INV25110018", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(546), 5000000m, "Complete", 0m, 5000000m, 0m, 10m },
                    { 19, 19, "INV25110019", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(547), 3000000m, "Complete", 0m, 9000000m, 0m, 10m },
                    { 20, 20, "INV25110020", new DateTime(2025, 11, 20, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(549), 2400000m, "Complete", 0m, 2400000m, 0m, 10m }
                });

            migrationBuilder.InsertData(
                table: "RoomStatusLogs",
                columns: new[] { "LogID", "ChangedBy", "ChangedDate", "NewStatus", "OldStatus", "Reason", "RoomID" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 11, 15, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(600), "Booked", "Available", "Đặt phòng HTL25110001", 1 },
                    { 2, 3, new DateTime(2025, 11, 19, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(604), "Occupied", "Available", "Check-in khách walk-in", 2 },
                    { 3, 4, new DateTime(2025, 11, 17, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(606), "Booked", "Available", "Đặt phòng HTL25110003", 3 },
                    { 4, 2, new DateTime(2025, 11, 18, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(608), "Occupied", "Booked", "Check-in sớm", 4 },
                    { 5, 3, new DateTime(2025, 11, 16, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(609), "Booked", "Available", "Đặt phòng HTL25110005", 5 },
                    { 6, 4, new DateTime(2025, 11, 14, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(611), "Occupied", "Booked", "Check-in", 6 },
                    { 7, 2, new DateTime(2025, 11, 13, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(613), "Available", "Occupied", "Check-out", 7 },
                    { 8, 3, new DateTime(2025, 11, 12, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(615), "Booked", "Available", "Đặt phòng HTL25110008", 8 },
                    { 9, 1, new DateTime(2025, 11, 10, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(618), "Available", "Maintenance", "Sửa chữa xong", 9 },
                    { 10, 4, new DateTime(2025, 11, 11, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(619), "Booked", "Available", "Đặt phòng", 10 },
                    { 11, 2, new DateTime(2025, 11, 9, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(621), "Occupied", "Booked", "Check-in", 11 },
                    { 12, 3, new DateTime(2025, 11, 8, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(623), "Available", "Occupied", "Check-out", 12 },
                    { 13, 4, new DateTime(2025, 11, 7, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(624), "Booked", "Available", "Đặt phòng", 13 },
                    { 14, 2, new DateTime(2025, 11, 6, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(626), "Occupied", "Booked", "Check-in", 14 },
                    { 15, 3, new DateTime(2025, 11, 5, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(628), "Available", "Occupied", "Check-out", 15 },
                    { 16, 4, new DateTime(2025, 11, 4, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(629), "Booked", "Available", "Đặt phòng", 16 },
                    { 17, 2, new DateTime(2025, 11, 3, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(631), "Occupied", "Booked", "Check-in", 17 },
                    { 18, 3, new DateTime(2025, 11, 2, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(633), "Available", "Occupied", "Check-out", 18 },
                    { 19, 4, new DateTime(2025, 11, 1, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(635), "Booked", "Available", "Đặt phòng", 19 },
                    { 20, 2, new DateTime(2025, 10, 31, 9, 58, 48, 192, DateTimeKind.Local).AddTicks(636), "Occupied", "Booked", "Check-in", 20 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BookingCode",
                table: "Booking",
                column: "BookingCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserID",
                table: "Booking",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingID",
                table: "BookingDetails",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_RoomTypeID",
                table: "BookingDetails",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "UX_BookingDetail_Room_Period",
                table: "BookingDetails",
                columns: new[] { "RoomID", "CheckInDate", "CheckOutDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BookingID",
                table: "Invoice",
                column: "BookingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceCode",
                table: "Invoice",
                column: "InvoiceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomNumber",
                table: "Room",
                column: "RoomNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_RoomTypeID",
                table: "Room",
                column: "RoomTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Room_Status_Type",
                table: "Room",
                columns: new[] { "Status", "RoomTypeID" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomStatusLogs_ChangedBy",
                table: "RoomStatusLogs",
                column: "ChangedBy");

            migrationBuilder.CreateIndex(
                name: "IX_RoomStatusLogs_RoomID",
                table: "RoomStatusLogs",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypes_TypeName",
                table: "RoomTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdCard",
                table: "Users",
                column: "IdCard",
                unique: true,
                filter: "[IdCard] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "RoomStatusLogs");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
