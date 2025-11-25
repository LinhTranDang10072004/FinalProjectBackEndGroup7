using HotelManagementApi.Models;
using System.ComponentModel.DataAnnotations;

public class RoomStatusLog
{
    [Key] public int LogID { get; set; }
    public string OldStatus { get; set; } = null!;
    public string NewStatus { get; set; } = null!;
    public DateTime ChangedDate { get; set; } = DateTime.Now;
    public int? ChangedBy { get; set; }
    [MaxLength(500)] public string? Reason { get; set; }
    public int RoomID { get; set; }
    public Room Room { get; set; } = null!;
    public User? ChangedByUser { get; set; }
}