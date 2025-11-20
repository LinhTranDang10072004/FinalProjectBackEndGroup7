using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HotelManagementApi.Models;

namespace HotelManagementApi.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<RoomType> RoomTypes => Set<RoomType>();
        public DbSet<Room> Rooms => Set<Room>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<BookingDetail> BookingDetails => Set<BookingDetail>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<RoomStatusLog> RoomStatusLogs => Set<RoomStatusLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserID);
                entity.HasIndex(u => u.Phone).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.IdCard).IsUnique().HasFilter("[IdCard] IS NOT NULL");

                entity.Property(u => u.Gender).HasDefaultValue("Other");
                entity.Property(u => u.UserRole).HasDefaultValue("Customer").HasMaxLength(20).IsRequired();

                entity.ToTable(t => t.HasCheckConstraint("CK_Users_Role",
                 "UserRole IN ('Admin', 'Receptionist', 'Customer')"));

                entity.ToTable(t => t.HasCheckConstraint("CK_Users_Gender",
                    "Gender IN ('Male', 'Female', 'Other')"));
            });

            // RoomType
            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomTypes");
                entity.HasKey(e => e.RoomTypeID);
                entity.HasIndex(e => e.TypeName).IsUnique();
            });

            // Room
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(r => r.RoomNumber).IsUnique();
                entity.HasIndex(r => new { r.Status, r.RoomTypeID }).HasDatabaseName("IX_Room_Status_Type");
                entity.Property(r => r.Status).HasDefaultValue("Available").HasMaxLength(20).IsRequired();

                entity.ToTable("Room", t => t
                    .HasCheckConstraint("CK_Room_Status", "Status IN ('Available', 'Booked', 'Occupied', 'Maintenance')")
                );

                entity.HasOne(r => r.RoomType)
                      .WithMany(rt => rt.Rooms)
                      .HasForeignKey(e => e.RoomTypeID)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Booking
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasIndex(b => b.BookingCode).IsUnique();
                entity.Property(b => b.BookingStatus).HasDefaultValue("Confirmed").HasMaxLength(20).IsRequired();

                entity.ToTable("Booking", t => t
                    .HasCheckConstraint("CK_Booking_Status", "BookingStatus IN ('Confirmed', 'Check-in', 'Check-out', 'Cancelled', 'No-show')")
                );

                entity.HasOne(b => b.User)
                      .WithMany(u => u.Bookings)
                      .HasForeignKey("UserID")
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Invoice
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasIndex(i => i.InvoiceCode).IsUnique();
                entity.Property(i => i.PaymentStatus).HasDefaultValue("Complete").HasMaxLength(20).IsRequired();

                entity.ToTable("Invoice", t => t
                    .HasCheckConstraint("CK_Invoice_PaymentStatus", "PaymentStatus IN ('Complete')")
                );

                entity.HasOne(i => i.Booking)
                      .WithOne(b => b.Invoice)
                      .HasForeignKey<Invoice>("BookingID")
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // BookingDetail
            modelBuilder.Entity<BookingDetail>(entity =>
            {
                entity.ToTable("BookingDetails");

                entity.HasKey(e => e.BookingDetailID);

                entity.HasOne(e => e.Booking)
                      .WithMany(b => b.BookingDetails)
                      .HasForeignKey(e => e.BookingID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Room)
                      .WithMany(r => r.BookingDetails)
                      .HasForeignKey(e => e.RoomID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.RoomType)
                      .WithMany()
                      .HasForeignKey(e => e.RoomTypeID)
                      .OnDelete(DeleteBehavior.Restrict);

                // Index tránh đặt cùng phòng cùng thời gian
                entity.HasIndex(e => new { e.RoomID, e.CheckInDate, e.CheckOutDate })
                      .IsUnique()
                      .HasDatabaseName("UX_BookingDetail_Room_Period");

                // Check Constraint
                entity.ToTable(t => t.HasCheckConstraint("CK_BookingDetail_DateRange",
                    "CheckOutDate > CheckInDate"));

                entity.ToTable(t => t.HasCheckConstraint("CK_BookingDetail_Price",
                    "PricePerNight > 0"));
            });

            // RoomStatusLog
            modelBuilder.Entity<RoomStatusLog>(entity =>
            {
                entity.ToTable("RoomStatusLogs");

                entity.HasOne(l => l.Room)
                      .WithMany(r => r.StatusLogs)
                      .HasForeignKey("RoomID")
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(l => l.ChangedBy).IsRequired(false);

                entity.HasOne(l => l.ChangedByUser)
                      .WithMany(u => u.RoomStatusLogs)
                      .HasForeignKey(l => l.ChangedBy)
                      .OnDelete(DeleteBehavior.SetNull)
                      .IsRequired(false);
            });
            
            SendData.Seed(modelBuilder);
        }
    }
}