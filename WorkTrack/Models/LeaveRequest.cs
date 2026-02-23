using WorkTrack.Models.Enum;

namespace WorkTrack.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public int LeaveTypeId { get; set; }

        public LeaveType LeaveType { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalDays { get; set; }

        public string Reason { get; set; } = string.Empty;

        public LeaveStatus Status { get; set; }

        public string? ApprovedById { get; set; }

        public User? ApprovedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ReviewedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
    }
}
