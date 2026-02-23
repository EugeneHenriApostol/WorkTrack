namespace WorkTrack.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public User User { get; set; } = null!;

        public int LeaveTypeId { get; set; }

        public LeaveType LeaveType { get; set; } = null!;

        public int TotalDays { get; set; }

        public int UsedDays { get; set; }
        public int Year { get; set; }

        public int RemainingDays => TotalDays - UsedDays;
    }
}
